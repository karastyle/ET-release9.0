namespace ET.Client
{ 
    [ChildOf(typeof(ModeMgr))]
    public class LoginMode : Entity, IAwake, IUpdate, IDestroy,
            IDynamicEvent<EnterPlayModeEvent>, IDynamicEvent<ExitPlayModeEvent>,
            IDynamicEvent<LoginGameEvent>, IDynamicEvent<SelectServerEvent>
    {
        public CPlayMode playMode = CPlayMode.Login;
    }
}

