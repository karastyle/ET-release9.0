

using System.Collections.Generic;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_LoginGameGateHandler : MessageSessionHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response)
        {
            Scene root = session.Root();
            
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().NoContext();
                return;
            }
            
            string account = root.GetComponent<GateSessionKeyComponent>().Get(request.Key);
            if (account == null)
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                response.Message = "Gate Key 验证失败";
                session?.Disconnect().NoContext();
                return;
            }
            
            //移除令牌，避免重复使用
            root.GetComponent<GateSessionKeyComponent>().Remove(request.Key);
            //gate session的自动断开倒计时要关了
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            long instanceId = session.InstanceId;
            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();
            //添加session锁
            using (session.AddComponent<SessionLockingComponent>())
            {
                //使用协程锁，这里是避免多个客户端同时对数据库进行异步操作，如果已经进入了这里的异步逻辑块，那么其他要进入的就必须进行排队，等处理完第一个再依次执行队列中的
                using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, request.AccountName.GetLongHashCode()))
                {
                    //异步过程中，如果instanceId变了，说明session可能断开或者复用了
                    if (instanceId != session.InstanceId)
                    {
                        response.Error = ErrorCode.ERR_LoginGameGateError01;
                    }
                    
                    //通知登录中心服， 记录本次登录的服务器zone
                    G2L_AddLoginRecord g2LAddLoginRecord = G2L_AddLoginRecord.Create();
                    g2LAddLoginRecord.AccountName = request.AccountName;
                    g2LAddLoginRecord.ServerId = root.Zone();
                    
                    List<StartSceneConfig> loginCenterConfig = StartSceneConfigCategory.Instance.GetBySceneType(SceneType.LoginCenter);
                    L2G_AddLoginRecord l2GAddLoginRecord = (L2G_AddLoginRecord) await root.GetComponent<MessageSender>()
                            .Call(loginCenterConfig[0].ActorId, g2LAddLoginRecord);

                    if (l2GAddLoginRecord.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2GAddLoginRecord.Error;
                        session?.Disconnect().NoContext();
                        return;
                    }

                    //Player 存储了账号，玩家登录的角色id
                    PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
                    Player player = playerComponent.GetByAccount(account);
                    if (player == null)
                    {
                        player = playerComponent.AddChild<Player, string>(account);
                        player.RoleId = request.RoleId;
                        
                        PlayerSessionComponent playerSessionComponent = player.AddComponent<PlayerSessionComponent>();
                        playerSessionComponent.AddComponent<MailBoxComponent, int>(MailBoxType.GateSession);
                        await playerSessionComponent.AddLocation(LocationType.GateSession);
			
                        //player也要能发送消息，消息类型不同
                        player.AddComponent<MailBoxComponent, int>(MailBoxType.UnOrderedMessage);
                        //注册位置
                        await player.AddLocation(LocationType.Player);
			
                        //两者互相引用
                        session.AddComponent<SessionPlayerComponent>().Player = player;
                        playerSessionComponent.Session = session;

                        player.PlayerState = PlayerState.Gate;
                    }
                    else
                    {
                        //如果玩家已经创建出来了，此时可能在下线倒计时中，这里再次登录要把下线倒计时给关了
                        player.RemoveComponent<PlayerOfflineOutTimeComponet>();

                        session.AddComponent<SessionPlayerComponent>().Player = player;
                        player.GetComponent<PlayerSessionComponent>().Session = session;
                    }

                    response.PlayerId = player.Id;
                }
            }
        }
    }
}