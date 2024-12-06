
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [EntitySystemOf(typeof(ServerInfoComponent))]
    public static partial class ServerInfoComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.ServerInfoComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.ServerInfoComponent self)
        {

        }
        
        public static ServerInfo Get(this ServerInfoComponent self, long id)
        {
            ServerInfo info = self.GetChild<ServerInfo>(id);
            return info;
        }

        public static void Remove(this ServerInfoComponent self, long id)
        {
            ServerInfo info = self.GetChild<ServerInfo>(id);
            info?.Dispose();
        }

        public static void RemoveAll(this ServerInfoComponent self)
        {
            foreach (var key in self.Children.Keys.ToList())
            {
                self.Children.Remove(key);
            }
        }

        public static List<EntityRef<ServerInfo>> GetServerList(this ServerInfoComponent self)
        {
            List<EntityRef<ServerInfo>> list = new();
            foreach (Entity child in self.Children.Values)
            {
                ServerInfo info = child as ServerInfo;
                list.Add(info);
            }
            return list;
        }
    }
}