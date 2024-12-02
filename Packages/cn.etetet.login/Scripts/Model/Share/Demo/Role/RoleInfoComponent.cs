using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class RoleInfoComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<long, EntityRef<RoleInfo>> dictionary = new();
    }
}