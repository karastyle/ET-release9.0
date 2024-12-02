namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOf(typeof(RoleInfo))]
    public class C2R_GetRolesHandler : MessageSessionHandler<C2R_GetRoles, R2C_GetRoles>
    {
        protected override async ETTask Run(Session session, C2R_GetRoles request, R2C_GetRoles response)
        {
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
                using (await coroutineLockComponent.Wait(CoroutineLockType.CreateRole, request.Account.GetLongHashCode()))
                {
                    DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                    var roleInfos = await dbComponent.Query<RoleInfo>(d => d.Account == request.Account
                            && d.ServerId == request.ServerId
                            && d.State == (int)RoleInfoState.Normal);
                    if (roleInfos == null || roleInfos.Count == 0)
                    {
                        return;
                    }

                    foreach (var roleInfo in roleInfos)
                    {
                        response.RoleInfo.Add(roleInfo.ToMessage());
                    }
                }
            }
        }
    }
}