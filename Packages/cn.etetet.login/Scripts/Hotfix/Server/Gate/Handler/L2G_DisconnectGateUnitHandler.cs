namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class L2G_DisconnectGateUnitHandler: MessageHandler<Scene, L2G_DisconnectGateUnit, G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response)
        {
            CoroutineLockComponent coroutineLockComponent = scene.GetComponent<CoroutineLockComponent>();
            //使用协程锁，这里是避免多个客户端同时对数据库进行异步操作，如果已经进入了这里的异步逻辑块，那么其他要进入的就必须进行排队，等处理完第一个再依次执行队列中的
            using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, request.AccountName.GetLongHashCode()))
            {
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player player = playerComponent.GetByAccount(request.AccountName);

                if (player == null)
                {
                    return;
                }
                
                scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountName.GetLongHashCode());
                Session gateSession = player.GetComponent<PlayerSessionComponent>()?.Session;
                if (gateSession != null && !gateSession.IsDisposed)
                {
                    A2C_Disconnect a2CDisconnect = A2C_Disconnect.Create();
                    a2CDisconnect.Error = ErrorCode.ERR_OtherAccountLogin;
                    gateSession.Send(a2CDisconnect);
                    gateSession.Disconnect().NoContext();
                }

                if (player.GetComponent<PlayerSessionComponent>()?.Session != null)
                {
                    player.GetComponent<PlayerSessionComponent>().Session = null;
                }
    
                //10s后玩家下线
                player.AddComponent<PlayerOfflineOutTimeComponet>();
            }
        }
    }
}

