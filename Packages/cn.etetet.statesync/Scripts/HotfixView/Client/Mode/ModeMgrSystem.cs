namespace ET.Client
{
    [EntitySystemOf(typeof(ModeMgr))]
    public static partial class ModeMgrSystem
    {
        [EntitySystem]
        private static void Awake(this ModeMgr self)
        {
            self.AddChild<LoginMode>();
            self.AddChild<CreateMode>();
            self.AddChild<LobbyMode>();
        }

        public static void EnterGameMode(this ModeMgr self, CPlayMode playMode)
        {
            self.curPlayMode = playMode;
            
            self.DynamicEvent(new EnterPlayMode(playMode)).NoContext();
        }
        
        public static void ExitGameMode(this ModeMgr self, CPlayMode playMode)
        {
            self.DynamicEvent(new ExitPlayMode(playMode)).NoContext();
        }
    }
}

