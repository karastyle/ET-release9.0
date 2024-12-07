
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [EntitySystemOf(typeof(ServerInfoModel))]
    public static partial class ServerInfoModelSystem
    {
        [EntitySystem]
        private static void Awake(this ET.ServerInfoModel self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.ServerInfoModel self)
        {

        }
        
        public static ServerInfo Get(this ServerInfoModel self, long id)
        {
            ServerInfo info = self.GetChild<ServerInfo>(id);
            return info;
        }

        public static void Remove(this ServerInfoModel self, long id)
        {
            //Dispose 会自动把Entity从父节点移除
            ServerInfo info = self.GetChild<ServerInfo>(id);
            info?.Dispose();
        }

        public static void RemoveAll(this ServerInfoModel self)
        {
            foreach (var value in self.Children.Values.ToList())
            {
                value?.Dispose();
            }
        }

        public static List<EntityRef<ServerInfo>> GetServerList(this ServerInfoModel self)
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