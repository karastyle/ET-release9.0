namespace ET.Client
{
    [ChildOf(typeof(ModeMgr))]
    public class LobbyMode : Entity, IAwake, IUpdate, IDestroy, 
            IDynamicEvent<EnterPlayMode>, IDynamicEvent<ExitPlayMode>
    {
        public CPlayMode playMode = CPlayMode.Lobby;
    }
}