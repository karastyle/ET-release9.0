namespace ET.Client
{
    [EntitySystemOf(typeof(CreateMode))]
    [FriendOf(typeof(CreateMode))]
    public static partial class CreateModeSystem
    {
        [EntitySystem]
        private static async ETTask DynamicEvent(this CreateMode self, EnterPlayModeEvent message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }
        }

        [EntitySystem]
        private static async ETTask DynamicEvent(this CreateMode self, ExitPlayModeEvent message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }


        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this CreateMode self, CreateCharacterEvent message)
        {
            //创角
            
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this CreateMode self, EnterGameEvent message)
        {
            //进入游戏
            
        }
        
        [EntitySystem]
        private static void Awake(this ET.Client.CreateMode self)
        {
        }

        [EntitySystem]
        private static void Update(this ET.Client.CreateMode self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ET.Client.CreateMode self)
        {
        }
    }
}