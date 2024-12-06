using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [EntitySystemOf(typeof(RoleInfoComponent))]
    public static partial class RoleInfoComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.RoleInfoComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ET.RoleInfoComponent self)
        {
        }

        public static RoleInfo Get(this RoleInfoComponent self, long id)
        {
            RoleInfo info = self.GetChild<RoleInfo>(id);
            return info;
        }

        public static void Remove(this RoleInfoComponent self, long id)
        {
            RoleInfo info = self.GetChild<RoleInfo>(id);
            info?.Dispose();
        }

        public static void RemoveAll(this RoleInfoComponent self)
        {
            foreach (var key in self.Children.Keys.ToList())
            {
                self.Children.Remove(key);
            }
        }

        public static List<EntityRef<RoleInfo>> GetRoleList(this RoleInfoComponent self)
        {
            List<EntityRef<RoleInfo>> list = new();
            foreach (Entity child in self.Children.Values)
            {
                RoleInfo roleInfo = child as RoleInfo;
                //非冻结状态的角色
                if (roleInfo.State != (int)RoleInfoState.Freeze)
                {
                    list.Add(roleInfo);
                }
            }
            return list;
        }
    }
}