
using System.Collections.Generic;

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
        
        public static RoleInfo Get(this RoleInfoComponent self, long key)
        {
            if (!self.dictionary.TryGetValue(key, out EntityRef<RoleInfo> value))
            {
                return null;
            }

            return value;
        }

        public static void Add(this RoleInfoComponent self, long key, EntityRef<RoleInfo> value)
        {
            if (self.dictionary.ContainsKey(key))
            {
                self.dictionary[key] = value;
                return;
            }

            self.dictionary.Add(key, value);
        }

        public static void Remove(this RoleInfoComponent self, long key)
        {
            if (self.dictionary.ContainsKey(key))
            {
                self.dictionary.Remove(key);
            }
        }

        public static bool IsExist(this RoleInfoComponent self, long key)
        {
            return self.dictionary.ContainsKey(key);
        }
        
    }
}