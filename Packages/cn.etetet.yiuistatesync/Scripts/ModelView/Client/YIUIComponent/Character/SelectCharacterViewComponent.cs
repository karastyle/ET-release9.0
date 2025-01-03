﻿using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.2
    /// Desc
    /// </summary>
    public partial class SelectCharacterViewComponent : Entity, IDynamicEvent<DeleteRoleEvent>
    {
        public List<EntityRef<RoleUnit>>                      RoleDataList;
        public EntityRef<YIUILoopScrollChild> m_RoleLoop;
        public YIUILoopScrollChild            RoleLoop => m_RoleLoop;
    }
}
