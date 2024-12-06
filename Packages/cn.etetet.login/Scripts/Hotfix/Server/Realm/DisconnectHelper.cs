using System.Collections.Generic;

namespace ET.Server
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            long instanceId = self.InstanceId;
            //等待1s   不能马上关闭session，因为外部还需要这个session把错误码返回给客户端
            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();
            await timerComponent.WaitAsync(1000);

            if (self.InstanceId != instanceId)
            {
                //如果1s后， id不一样了， 说明这个session可能已经销毁了，或者被重新复用了
                return;
            }
            self.Dispose();
        }

        public static async ETTask KickPlayer(Player player)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            long instanceId = player.InstanceId;
            CoroutineLockComponent coroutineLockComponent = player.Root().GetComponent<CoroutineLockComponent>();
            //使用协程锁，这里是避免多个客户端同时对数据库进行异步操作，如果已经进入了这里的异步逻辑块，那么其他要进入的就必须进行排队，等处理完第一个再依次执行队列中的
            using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, player.Account.GetLongHashCode()))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                {
                    return;
                }

                await KickPlayerNoLock(player);
            }
        }

        public static async ETTask KickPlayerNoLock(Player player)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            switch (player.PlayerState)     
            {
                case PlayerState.Disconnect:
                    break;
                case PlayerState.Gate:
                    break;
                case PlayerState.Game:
                    //通知游戏服下线unit角色， 并将数据存入数据库
                    var m2GRequestExitGame = (M2G_RequestExitGame)await player.Root().GetComponent<MessageLocationSenderComponent>()
                            .Get(LocationType.Unit).Call(player.RoleId, G2M_RequestExitGame.Create());
                    
                    //通知移除账号登录信息
                    G2L_RemoveLoginRecord g2LRemoveLoginRecord = G2L_RemoveLoginRecord.Create();
                    g2LRemoveLoginRecord.AccountName = player.Account;
                    g2LRemoveLoginRecord.ServerId = player.Zone();
                    List<StartSceneConfig> loginCenterConfig = StartSceneConfigCategory.Instance.GetBySceneType(SceneType.LoginCenter);
                    var L2G_removeLoginRecord = (L2G_RemoveloginRecord)await player.Root().GetComponent<MessageSender>()
                            .Call(loginCenterConfig[0].ActorId, g2LRemoveLoginRecord);
                    break;
            }

            TimerComponent timerComponent = player.Root().GetComponent<TimerComponent>();
            player.PlayerState = PlayerState.Disconnect;

            await player.GetComponent<PlayerSessionComponent>().RemoveLocation(LocationType.GateSession);
            await player.RemoveLocation(LocationType.Player);
            player.Root().GetComponent<PlayerComponent>()?.Remove(player.Id);
            player.Dispose();

            await timerComponent.WaitAsync(300);
             
        }
    }
}

