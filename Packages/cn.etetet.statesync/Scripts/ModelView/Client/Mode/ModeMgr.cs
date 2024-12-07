namespace ET.Client
{
    public enum CPlayMode {
        None,
        Login,//登录 
        Create,//创角
        Lobby, //主城
    }

    [ComponentOf(typeof(Scene))]
    public class ModeMgr: Entity, IAwake
    {
        //当前模式
        public  CPlayMode curPlayMode = CPlayMode.None;
    }
    
}

