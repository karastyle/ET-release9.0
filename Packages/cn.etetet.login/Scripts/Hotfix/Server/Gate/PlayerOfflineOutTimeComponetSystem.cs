
namespace ET.Server
{
    [Invoke(TimerInvokeType.PlayerOfflineOutTime)]
    public class PlayerOfflineOutTIme : ATimer<PlayerOfflineOutTimeComponet>
    {
        protected override void Run(PlayerOfflineOutTimeComponet t)
        {
            t?.KickPlayer();
        }
    }

    [EntitySystemOf(typeof(PlayerOfflineOutTimeComponet))]
    public static partial class PlayerOfflineOutTimeComponetSystem
    {
        [EntitySystem]
        private static void Awake(this PlayerOfflineOutTimeComponet self)
        {
            //移除定时器
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
            //10s后， 下线
            self.Timer = self.Root().GetComponent<TimerComponent>()
                    .NewOnceTimer(TimeInfo.Instance.ServerNow() + 100000, TimerInvokeType.PlayerOfflineOutTime, self);
        }

        [EntitySystem]
        private static void Destroy(this PlayerOfflineOutTimeComponet self)
        {
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
        }

        public static void KickPlayer(this PlayerOfflineOutTimeComponet self)
        {
            DisconnectHelper.KickPlayer(self.GetParent<Player>()).NoContext();
        }
    }
}

