namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOf(typeof(RoleUnit))]
    public class C2R_DeleteRoleHandler : MessageSessionHandler<C2R_DeleteRole, R2C_DeleteRole>
    {
        protected override async ETTask Run(Session session, C2R_DeleteRole request, R2C_DeleteRole response)
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
                using (await coroutineLockComponent.Wait(CoroutineLockType.CreateRole, request.Account.GetLongHashCode()))
                {
                    DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                    var roleInfos = await dbComponent.Query<RoleUnit>(d => d.Id == request.RoleInfoId
                            && d.ServerId == request.ServerId);
                    if (roleInfos == null || roleInfos.Count == 0)
                    {
                        response.Error = ErrorCode.ERR_RoleNotExist;
                        return;
                    }

                    //添加到session节点中，这样才会进入到生命周期管理，避免下面的数据库异步操作完成前被释放
                    var roleInfo = roleInfos[0];
                    session.AddChild(roleInfo);

                    //删除角色并不是从数据库删除， 而是改为冻结状态，为了后续可以找回角色
                    roleInfo.State = (int)RoleInfoState.Freeze;

                    await dbComponent.Save(roleInfo);
                    response.DeletedRoleInfoId = roleInfo.Id;
                    
                    session.RemoveChild(roleInfo.Id);
                }

            }
        }
    }
}