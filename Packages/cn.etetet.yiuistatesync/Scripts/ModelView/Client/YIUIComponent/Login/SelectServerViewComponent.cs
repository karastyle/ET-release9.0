using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.1
    /// Desc
    /// </summary>
    public partial class SelectServerViewComponent : Entity
    {
        public List<EntityRef<ServerInfo>>                      ServerDataList;
        public EntityRef<YIUILoopScrollChild> m_ServerLoop;
        public YIUILoopScrollChild            ServereLoop => m_ServerLoop;
    }
}
