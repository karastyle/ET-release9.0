namespace ET.Server
{
    [EntitySystemOf(typeof(ServerInfoManagerComponent))]
    [FriendOf(typeof(ServerInfoManagerComponent))]
    [FriendOf(typeof(ServerInfo))]
    public static partial class ServerInfoManagerComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ServerInfoManagerComponent self)
        {
            self.Load();
        }
        
        [EntitySystem]
        private static void Destroy(this ServerInfoManagerComponent self)
        {
            foreach (var serverInfoRef in self.ServerInfos)
            {
                ServerInfo serverInfo = serverInfoRef;
                serverInfo?.Dispose();
            }
            self.ServerInfos.Clear();
        }

        public static void Load(this ServerInfoManagerComponent self)
        {
            foreach (var serverInfoRef in self.ServerInfos)
            {
                ServerInfo serverInfo = serverInfoRef;
                serverInfo?.Dispose();
            }
            self.ServerInfos.Clear();

            var serverInfoConfigs = StartZoneConfigCategory.Instance.GetAll();

            foreach (var info in serverInfoConfigs.Values)
            {
                if (info.ZoneType != 100)
                {
                    continue;
                }

                //把所有游戏服，都存起来
                ServerInfo newServerInfo = self.AddChildWithId<ServerInfo>(info.Id);
                newServerInfo.ServerName = info.DBName;
                newServerInfo.Status = (int)ServerStatus.Normal;
                newServerInfo.ServerId = info.Id;
                self.ServerInfos.Add(newServerInfo);
            }
        }
    }
}

