namespace ET.Client
{
    [ChildOf(typeof(ModeMgr))]
    public class LobbyMode : Entity, IAwake, IUpdate, IDestroy, 
            IDynamicEvent<EnterPlayModeEvent>, IDynamicEvent<ExitPlayModeEvent>
    {
        public CPlayMode playMode = CPlayMode.Lobby;
    }
}