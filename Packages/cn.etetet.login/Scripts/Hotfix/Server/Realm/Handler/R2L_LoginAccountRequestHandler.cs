namespace ET.Server
{
    [MessageHandler(SceneType.LoginCenter)]
    public class R2L_LoginAccountRequestHandler : MessageHandler<Scene, R2L_LoginAccountRequest, L2R_LoginAccountRequest>
    {
        protected override async ETTask Run(Scene scene, R2L_LoginAccountRequest request, L2R_LoginAccountRequest response)
        {
            long accountId = request.AccountName.GetLongHashCode();
            
           
            CoroutineLockComponent coroutineLockComponent = scene.GetComponent<CoroutineLockComponent>();
            //使用协程锁，这里是避免多个客户端同时对数据库进行异步操作，如果已经进入了这里的异步逻辑块，那么其他要进入的就必须进行排队，等处理完第一个再依次执行队列中的
            using (await coroutineLockComponent.Wait(CoroutineLockType.LoginCenterLock, accountId))
            {
                if (!scene.GetComponent<LoginInfoRecordComponent>().IsExist(accountId))
                {
                    //如果没有登录其他gate服务器，则返回suc
                    response.Error = ErrorCode.ERR_Success;
                    return;
                }

                //如果这个账号已经登录到gate了，则发送下线的消息
                int zone = scene.GetComponent<LoginInfoRecordComponent>().Get(accountId);
                StartSceneConfig gateConfig = RealmGateAddressHelper.GetGate(zone, request.AccountName);
                
                L2G_DisconnectGateUnit l2GDisconnectGateUnit = L2G_DisconnectGateUnit.Create();
                l2GDisconnectGateUnit.AccountName = request.AccountName;
                var g2LDisconnectGateUnit =
                        (G2L_DisconnectGateUnit)await scene.GetComponent<MessageSender>().Call(gateConfig.ActorId, l2GDisconnectGateUnit);
                response.Error = g2LDisconnectGateUnit.Error;
            }
        }
    }
}