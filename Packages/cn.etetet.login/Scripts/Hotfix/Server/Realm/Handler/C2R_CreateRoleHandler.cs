namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOf(typeof(RoleUnit))]
    public class C2R_CreateRoleHandler : MessageSessionHandler<C2R_CreateRole, R2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2R_CreateRole request, R2C_CreateRole response)
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

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_RoleNameIsNull;
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
                    var roleInfos = await dbComponent.Query<RoleUnit>(d => d.Name == request.Name
                            && d.ServerId == request.ServerId);
                    if (roleInfos != null && roleInfos.Count > 0)
                    {
                        response.Error = ErrorCode.ERR_RoleNameSame;
                        return;
                    }
                    
                    //角色自增id
                    var blackBoardInfos = await dbComponent.Query<BlackBoardInfo>(d => d.Key == (int)BlackBoardEnum.IncrementRoleId);
                    BlackBoardInfo blackBoardInfo = null;
                    if (blackBoardInfos.Count == 0)
                    {
                        //如果没有就创建一个 ， 从1000开始自增
                        blackBoardInfo = session.AddChild<BlackBoardInfo>();
                        blackBoardInfo.Key = (int)BlackBoardEnum.IncrementRoleId;
                        blackBoardInfo.Value = 1000;
                    }
                    else
                    {
                        blackBoardInfo = blackBoardInfos[0];
                    }

                    long newRoleId = blackBoardInfo.Value + 1;
                    RoleUnit newRoleUnit = session.AddChildWithId<RoleUnit>(newRoleId);
                    newRoleUnit.Name = request.Name;
                    newRoleUnit.State = (int)RoleInfoState.Normal;
                    newRoleUnit.ServerId = request.ServerId;
                    newRoleUnit.Account = request.Account;
                    newRoleUnit.CreateTime = TimeInfo.Instance.ServerNow();
                    newRoleUnit.LastLoginTime = 0;
                    newRoleUnit.HeroId = request.HeroId;

                    response.RoleInfo = newRoleUnit.ToMessage();
                    
                    await dbComponent.Save<RoleUnit>(newRoleUnit);
                    session.RemoveChild(newRoleUnit.Id);

                    blackBoardInfo.Value = newRoleId;
                    await dbComponent.Save<BlackBoardInfo>(blackBoardInfo);
                    session.RemoveChild(blackBoardInfo.Id);
                }
            }
        }
    }
}

