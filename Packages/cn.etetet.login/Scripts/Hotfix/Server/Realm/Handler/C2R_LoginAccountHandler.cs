using System.Collections.Generic;
using System.Text.RegularExpressions;
using CommandLine;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    public class C2R_LoginAccountHandler : MessageSessionHandler<C2R_LoginAccount, R2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2R_LoginAccount request, R2C_LoginAccount response)
        {
            //移除关闭session的定时器 （默认创建session5s后自动关闭）， 这里登录了就要把定时器关了
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            //判断是否有session锁组件， 这里是防止同一个客户端，多次发起登录请求 （当前请求还未处理完的时候，后面的请求要返回错误码）
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().NoContext();
                return;
            }

            //账号密码是空的
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoIsNull;
                session.Disconnect().NoContext();
                return;
            }

            if (!Regex.IsMatch(request.Account.Trim(), @"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_AccountNameFormError;
                session.Disconnect().NoContext();
                return;
            }

            if (!Regex.IsMatch(request.Password.Trim(), @"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormError;
                session.Disconnect().NoContext();
                return;
            }

            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();
            //添加session锁
            using (session.AddComponent<SessionLockingComponent>())
            {
                //使用协程锁，这里是避免多个客户端同时对数据库进行异步操作，如果已经进入了这里的异步逻辑块，那么其他要进入的就必须进行排队，等处理完第一个再依次执行队列中的
                using (await coroutineLockComponent.Wait(CoroutineLockType.LoginAccount, request.Account.GetLongHashCode()))
                {
                    DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                    var accountInfoList = await dbComponent.Query<Account>(d => d.AccountName.Equals(request.Account));
                    Account account = null;
                    if (accountInfoList != null && accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        session.AddChild(account);
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountInBlackListError;
                            session.Disconnect().NoContext();
                            return;
                        }

                        if (account.Password != request.Password)
                        {
                            response.Error = ErrorCode.ERR_LoginPasswordError;
                            session.Disconnect().NoContext();
                            return;
                        }
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        account.AccountName = request.Account.Trim();
                        account.Password = request.Password;
                        account.CreateTime = TimeInfo.Instance.ServerNow();
                        account.AccountType = (int)AccountType.General;
                        await dbComponent.Save(account);
                    }

                    //请求登录中心服务器
                    R2L_LoginAccountRequest r2LLoginAccountRequest = R2L_LoginAccountRequest.Create();
                    r2LLoginAccountRequest.AccountName = request.Account;

                    //拿到登录中心的服务器配置
                    List<StartSceneConfig> loginCenterConfig = StartSceneConfigCategory.Instance.GetBySceneType(SceneType.LoginCenter);
                    //服务器之间， 用MessageSender就可以发送消息了
                    var loginAccountResponse =
                            await session.Fiber().Root.GetComponent<MessageSender>().Call(loginCenterConfig[0].ActorId, r2LLoginAccountRequest) as
                                    L2R_LoginAccountRequest;

                    if (loginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;
                        session?.Disconnect().NoContext();
                        account?.Dispose();
                        return;
                    }

                    Session otherSession = session.Root().GetComponent<AccountSessionsComponent>().Get(request.Account);
                    //顶号登录，断掉之前的
                    otherSession?.Send(A2C_Disconnect.Create());
                    otherSession?.Disconnect().NoContext();

                    //记录登录中心的session
                    session.Root().GetComponent<AccountSessionsComponent>().Add(request.Account, session);
                    //session会在一定时间后断开
                    session.AddComponent<AccountCheckOutTimeComponent, string>(request.Account);

                    //生成token  token会在一定时间后失效
                    string Token = TimeInfo.Instance.ServerNow() + RandomGenerator.RandomNumber(int.MinValue, int.MaxValue).ToString();
                    session.Root().GetComponent<TokenComponent>().Remove(request.Account);
                    session.Root().GetComponent<TokenComponent>().Add(request.Account, Token);

                    response.Token = Token;
                    account?.Dispose();
                }
            }
        }
    }
}