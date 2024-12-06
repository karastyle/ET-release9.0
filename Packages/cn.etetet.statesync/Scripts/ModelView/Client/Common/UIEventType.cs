namespace ET.Client
{
    
    //切换Mode
    public struct EnterPlayMode
    {
        public CPlayMode playMode;

        public EnterPlayMode(CPlayMode playMode)
        {
            this.playMode = playMode;
        }
    }
    
    //切换Mode
    public struct ExitPlayMode
    {
        public CPlayMode playMode;
        
        public ExitPlayMode(CPlayMode playMode)
        {
            this.playMode = playMode;
        }
    }
}
