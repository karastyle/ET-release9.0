namespace ET.Server
{
    [ComponentOf(typeof(Player))]
    public class PlayerOfflineOutTimeComponet: Entity, IAwake, IDestroy
    {
        public long Timer;
    }
}

