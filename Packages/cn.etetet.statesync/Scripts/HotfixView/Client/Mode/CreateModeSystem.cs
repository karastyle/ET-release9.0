namespace ET.Client
{
    [EntitySystemOf(typeof(CreateMode))]
    [FriendOf(typeof(CreateMode))]
    public static partial class CreateModeSystem
    {
        [EntitySystem]
        private static async ETTask DynamicEvent(this CreateMode self, EnterPlayMode message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }
        }

        [EntitySystem]
        private static async ETTask DynamicEvent(this CreateMode self, ExitPlayMode message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }


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