using System;
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
    public partial class CreateCharacterViewComponent : Entity
    {
        public int CurCareerId { get; set; }

        public EntityRef<YIUI3DDisplayChild> m_Display;
        public YIUI3DDisplayChild            Display => m_Display;
    }
}
