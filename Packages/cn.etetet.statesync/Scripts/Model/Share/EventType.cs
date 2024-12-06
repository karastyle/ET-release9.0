namespace ET.Client
{
    public struct SceneChangeStart
    {
    }
    
    public struct SceneChangeFinish
    {
    }
    
    public struct AfterCreateClientScene
    {
    }
    
    public struct AfterCreateCurrentScene
    {
    }

    public struct AppStartInitFinish
    {
    }

    public struct LoginFinish
    {
    }

    public struct EnterMapFinish
    {
    }

    public struct AfterUnitCreate
    {
        public Unit Unit;
    }
    
    //删除角色事件
    public struct DeleteRoleEvent
    {
    }
    
    //切到选角界面
    public struct SwitchSelectCharacterEvent
    {}
    
    //切到创建界面
    public struct SwitchCreateCharacterEvent
    {}
    
    //关闭选角界面
    public struct CloseCharacterEvent
    {}
}