
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
        
        public static ServerInfo Get(this ServerInfoComponent self, long key)
        {
            if (!self.dictionary.TryGetValue(key, out EntityRef<ServerInfo> value))
            {
                return null;
            }

            return value;
        }

        public static void Add(this ServerInfoComponent self, long key, EntityRef<ServerInfo> value)
        {
            if (self.dictionary.ContainsKey(key))
            {
                self.dictionary[key] = value;
                return;
            }

            self.dictionary.Add(key, value);
        }

        public static void Remove(this ServerInfoComponent self, long key)
        {
            if (self.dictionary.ContainsKey(key))
            {
                self.dictionary.Remove(key);
            }
        }

        public static bool IsExist(this ServerInfoComponent self, long key)
        {
            return self.dictionary.ContainsKey(key);
        }

        public static List<EntityRef<ServerInfo>> GetServerList(this ServerInfoComponent self)
        {
            List<EntityRef<ServerInfo>> list = new();
            foreach (var server in self.dictionary)
            {
                list.Add(server.Value);
            }
            return list;
        }
    }
}