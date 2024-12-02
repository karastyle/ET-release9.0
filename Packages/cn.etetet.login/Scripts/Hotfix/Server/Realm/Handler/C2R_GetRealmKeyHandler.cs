
namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    public class C2R_GetRealmKeyHandler : MessageSessionHandler<C2R_GetRealmKey, R2C_GetRealmKey>
    {
        protected override async ETTask Run(Session session, C2R_GetRealmKey request, R2C_GetRealmKey response)
        {
            //这个是避免同一个客户端，发起多次请求。 并且很多地方都会用这个session锁，比如获取角色、获取服务器列表等等
            //说明同一时间只能有一个这种类型的操作
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().NoContext();
                return;
            }

            string token = session.Root().GetComponent<TokenComponent>().Get(request.Account);
            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                session?.Disconnect().NoContext();
                return;
            }
            
            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();
            //添加session锁
            using (session.AddComponent<SessionLockingComponent>())
            {
                //使用协程锁，这里是避免多个客户端同时对数据库进行异步操作，如果已经进入了这里的异步逻辑块，那么其他要进入的就必须进行排队，等处理完第一个再依次执行队列中的
                using (await coroutineLockComponent.Wait(CoroutineLockType.LoginAccount, request.Account.GetLongHashCode()))
                {
                    //随机分配一个Gate
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(request.ServerId, request.Account);
                    Log.Debug($"gate address: {config}");
                    
                    //向gate请求一个key，客户端可以拿这个key连接gate
                    R2G_GetLoginKey r2GGetLoginKey = R2G_GetLoginKey.Create();
                    r2GGetLoginKey.Account = request.Account;
                    G2R_GetLoginKey g2RGetLoginKey =
                            (G2R_GetLoginKey)await session.Fiber().Root.GetComponent<MessageSender>().Call(config.ActorId, r2GGetLoginKey);
                    response.Address = config.InnerIPPort.ToString();
                    response.Key = g2RGetLoginKey.Key;
                    response.GateId = g2RGetLoginKey.GateId;
                    
                    //拿到令牌后，realm就可以断开了
                    session.Disconnect().NoContext();
                }
            }
        }
    }
}