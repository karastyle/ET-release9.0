using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class RoleInfoComponent : Entity, IAwake, IDestroy
    {
        //不要自己写一个dict去存RoleInfo， 一切都是实体，既然是实体就需要挂在父实体下， 受生命周期管理
    }
}