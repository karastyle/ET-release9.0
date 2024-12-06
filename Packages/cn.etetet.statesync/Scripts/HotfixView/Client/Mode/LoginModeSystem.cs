namespace ET.Client
{
    [EntitySystemOf(typeof(LoginMode))]
    [FriendOf(typeof(LoginMode))]
    public static partial class  LoginModeSystem
    {
        [EntitySystem]
        private static async ETTask DynamicEvent(this LoginMode self, EnterPlayMode message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }

            //打开登录界面
            await YIUIMgrComponent.Inst.Root.OpenPanelAsync<LoginPanelComponent>();
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this LoginMode self, ExitPlayMode message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }

      
        }
        
        [EntitySystem]
        private static void Awake(this ET.Client.LoginMode self)
        {
        }

        [EntitySystem]
        private static void Update(this ET.Client.LoginMode self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ET.Client.LoginMode self)
        {
        }
    }
}

