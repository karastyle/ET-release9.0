using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [EntitySystemOf(typeof(RoleUnitModel))]
    public static partial class RoleUnitModelSystem
    {
        [EntitySystem]
        private static void Awake(this ET.RoleUnitModel self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ET.RoleUnitModel self)
        {
        }

        public static RoleUnit Get(this RoleUnitModel self, long id)
        {
            RoleUnit roleUnit = self.GetChild<RoleUnit>(id);
            return roleUnit;
        }

        public static void Remove(this RoleUnitModel self, long id)
        {
            RoleUnit roleUnit = self.GetChild<RoleUnit>(id);
            roleUnit?.Dispose();
        }

        public static void RemoveAll(this RoleUnitModel self)
        {
            foreach (var key in self.Children.Keys.ToList())
            {
                self.Children.Remove(key);
            }
        }

        public static List<EntityRef<RoleUnit>> GetRoleList(this RoleUnitModel self)
        {
            List<EntityRef<RoleUnit>> list = new();
            foreach (Entity child in self.Children.Values)
            {
                RoleUnit roleUnit = child as RoleUnit;
                //非冻结状态的角色
                if (roleUnit.State != (int)RoleInfoState.Freeze)
                {
                    list.Add(roleUnit);
                }
            }
            return list;
        }
    }
}