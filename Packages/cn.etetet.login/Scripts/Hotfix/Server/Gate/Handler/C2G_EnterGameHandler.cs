using System;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_EnterGameHandler: MessageSessionHandler<C2G_EnterGame, G2C_EnterGame>
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response)
        {
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                return;
            }
            
            SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                response.Error = ErrorCode.ERR_SessionPlayerError;
                return;
            }

            Player player = sessionPlayerComponent.Player;
            if (player == null || player.IsDisposed)
            {
                response.Error = ErrorCode.ERR_NonePlayerError;
                return;
            }

            long instancdId = session.InstanceId;
            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();
            //添加session锁
            using (session.AddComponent<SessionLockingComponent>())
            {
                //使用协程锁，这里是避免多个客户端同时对数据库进行异步操作，如果已经进入了这里的异步逻辑块，那么其他要进入的就必须进行排队，等处理完第一个再依次执行队列中的
                using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, player.Account.GetLongHashCode()))
                {
                    if (instancdId != session.InstanceId || player.IsDisposed)
                    {
                        response.Error = ErrorCode.ERR_PlayerSessionError;
                        return;
                    }

                    if (player.PlayerState == PlayerState.Game)
                    {
                        try
                        {
                            //给map上的 unit 发送二次登录的消息
                            G2M_SecondLogin g2MSecondLogin = G2M_SecondLogin.Create();
                            IResponse reqEnter = await session.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit)
                                    .Call(player.RoleId, g2MSecondLogin);
                            if (reqEnter.Error == ErrorCode.ERR_Success)
                            {
                                Log.Console("二次登录逻辑，补发场景的消息");
                                return;
                            }

                            Log.Error($"二次登录失败： {reqEnter.Error} | {reqEnter.Message}");
                            response.Error = ErrorCode.ERR_ReEnterGameError;
                            await DisconnectHelper.KickPlayerNoLock(player);
                            session.Disconnect().NoContext();
                        }
                        catch (Exception e)
                        {
                            Log.Error($"二次登录失败： {e}");
                            response.Error = ErrorCode.ERR_ReEnterGameError2;
                            await DisconnectHelper.KickPlayerNoLock(player);
                            session.Disconnect().NoContext();
                            throw;
                        }

                        return;
                    }

                    try
                    {
                        //在Gate上动态创建一个临时的Map Scene， 把unit从db加载进来，然后传送到真正的map， 这样登录和传送的逻辑就一样了
                        // 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
                        GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
                        gateMapComponent.Scene =
                                await GateMapFactory.Create(gateMapComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");

                        Scene scene = gateMapComponent.Scene;
                        // 这里可以从DB中加载Unit   现在先直接创建
                        Unit unit = UnitFactory.Create(scene, player.Id, UnitType.Player);
                        long unitId = unit.Id;

                        StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Map1");

                        // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
                        TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.ActorId, startSceneConfig.Name).NoContext();
                        
                        player.RoleId = unitId;
                        response.MyUnitId = unitId;
                        player.PlayerState = PlayerState.Game;
                    }
                    catch (Exception e)
                    {
                        Log.Error($"角色进入游戏逻辑服出现问题 账号Id: {player.Account}  角色Id:{player.Id} 异常信息： {e}");
                        response.Error = ErrorCode.ERR_EnterGameError;
                        await DisconnectHelper.KickPlayerNoLock(player);
                        session.Disconnect().NoContext();;
                    }
                }
            }
        }
    }
}

