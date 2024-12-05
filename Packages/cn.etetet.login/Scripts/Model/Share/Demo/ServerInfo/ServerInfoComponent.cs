using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(ServerInfo))]
    [ComponentOf(typeof(Scene))]
    public class ServerInfoComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<long, EntityRef<ServerInfo>> dictionary = new();
    }
}