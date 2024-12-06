namespace ET.Client
{
    [ChildOf(typeof(ModeMgr))]
    public class CreateMode : Entity, IAwake, IUpdate, IDestroy,
            IDynamicEvent<EnterPlayMode>, IDynamicEvent<ExitPlayMode>
    {
        public CPlayMode playMode = CPlayMode.Create;
    }
}