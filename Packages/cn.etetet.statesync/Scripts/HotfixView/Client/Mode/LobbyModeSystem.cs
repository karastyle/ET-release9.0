namespace ET.Client
{
    [EntitySystemOf(typeof(LobbyMode))]
    [FriendOf(typeof(LobbyMode))]
    public static partial class LobbyModeSystem
    {
        [EntitySystem]
        private static async ETTask DynamicEvent(this LobbyMode self, EnterPlayMode message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }


        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this LobbyMode self, ExitPlayMode message)
        {
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }


        }
        
        [EntitySystem]
        private static void Awake(this ET.Client.LobbyMode self)
        {

        }
        [EntitySystem]
        private static void Update(this ET.Client.LobbyMode self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.LobbyMode self)
        {

        }
    }
}