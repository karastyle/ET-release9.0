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
    [FriendOf(typeof(CharacterPanelComponent))]
    public static partial class CharacterPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this CharacterPanelComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this CharacterPanelComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this CharacterPanelComponent self)
        {
            await self.UIPanel.OpenViewAsync<CreateCharacterViewComponent>();
            return true;
        }

        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}
