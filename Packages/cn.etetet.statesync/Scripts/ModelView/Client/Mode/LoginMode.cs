namespace ET.Client
{ 
    [ChildOf(typeof(ModeMgr))]
    public class LoginMode : Entity, IAwake, IUpdate, IDestroy,
            IDynamicEvent<EnterPlayMode>, IDynamicEvent<ExitPlayMode>
    {
        public CPlayMode playMode = CPlayMode.Login;
    }
}

