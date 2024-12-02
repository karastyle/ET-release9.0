namespace ET.Server
{
    [Invoke(TimerInvokeType.AccountSessionCheckOutTime)]
    public class AccountSessionCheckOutTimer : ATimer<AccountCheckOutTimeComponent>
    {
        protected override void Run(AccountCheckOutTimeComponent t)
        {
            t?.DeleteSession();
        }
    }

    [EntitySystemOf(typeof(AccountCheckOutTimeComponent))]
    public static partial class AccountCheckOutTimeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this AccountCheckOutTimeComponent self, string account)
        {
            self.Account = account;
            //移除定时器
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
            //10分钟后， 关闭session
            self.Timer = self.Root().GetComponent<TimerComponent>()
                    .NewOnceTimer(TimeInfo.Instance.ServerNow() + 600000, TimerInvokeType.AccountSessionCheckOutTime, self);
        }

        [EntitySystem]
        private static void Destroy(this AccountCheckOutTimeComponent self)
        {
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
        }

        public static void DeleteSession(this AccountCheckOutTimeComponent self)
        {
            Session session = self.GetParent<Session>();

            Session originSession = session.Root().GetComponent<AccountSessionsComponent>().Get(self.Account);
            //如果当前场景的session， 和定时器父节点session是同一个，那么当前场景可以移除session（否则，不用移除）
            if (originSession != null && session.InstanceId == originSession.InstanceId)
            {
                session.Root().GetComponent<AccountSessionsComponent>().Remove(self.Account);
            }

            //告诉客户端断开， 并关闭session
            A2C_Disconnect a2CDisconnect = A2C_Disconnect.Create();
            a2CDisconnect.Error = 1;
            session?.Send(a2CDisconnect);
            session?.Disconnect().NoContext();
        }
    }
}

