namespace ET.Client
{
    [ChildOf(typeof(ModeMgr))]
    public class CreateMode : Entity, IAwake, IUpdate, IDestroy,
            IDynamicEvent<EnterPlayModeEvent>, IDynamicEvent<ExitPlayModeEvent>,
            IDynamicEvent<CreateCharacterEvent>, IDynamicEvent<EnterGameEvent>
    {
        public CPlayMode playMode = CPlayMode.Create;
    }
}