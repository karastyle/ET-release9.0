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
            
            self.DynamicEvent(new EnterPlayModeEvent(playMode)).NoContext();
        }
        
        public static void ExitGameMode(this ModeMgr self, CPlayMode playMode)
        {
            self.DynamicEvent(new ExitPlayModeEvent(playMode)).NoContext();
        }
        
        //这里不提供获取具体Mode的接口， 因为外界并不知道当前处于哪个Mode
        //因此外界只能发出事件，由当前正在激活的Mode接受并处理流程相关的逻辑
    }
}

