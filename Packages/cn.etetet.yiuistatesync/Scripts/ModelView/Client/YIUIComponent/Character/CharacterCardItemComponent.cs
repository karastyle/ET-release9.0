using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.3
    /// Desc
    /// </summary>
    public partial class CharacterCardItemComponent : Entity
    {
        public EntityRef<RoleUnit> data { get; set; }
    }
}
