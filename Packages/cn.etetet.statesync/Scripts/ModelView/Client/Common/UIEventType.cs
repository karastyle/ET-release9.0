namespace ET.Client
{
    
    //切换Mode
    public struct EnterPlayModeEvent
    {
        public CPlayMode playMode;

        public EnterPlayModeEvent(CPlayMode playMode)
        {
            this.playMode = playMode;
        }
    }
    
    //退出Mode
    public struct ExitPlayModeEvent
    {
        public CPlayMode playMode;
        
        public ExitPlayModeEvent(CPlayMode playMode)
        {
            this.playMode = playMode;
        }
    }
    
    //登录
    public struct LoginGameEvent
    {
        public string account;
        public string password;
        public LoginGameEvent(string account, string password)
        {
            this.account = account;
            this.password = password;
        }
    }
    
    //选服
    public struct SelectServerEvent{}
    
    //创角
    public struct CreateCharacterEvent{}
    
    //进入游戏
    public struct EnterGameEvent{}
}
