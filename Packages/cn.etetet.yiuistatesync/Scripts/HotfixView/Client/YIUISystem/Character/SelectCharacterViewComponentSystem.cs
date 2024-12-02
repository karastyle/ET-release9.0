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
    [FriendOf(typeof(SelectCharacterViewComponent))]
    public static partial class SelectCharacterViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this SelectCharacterViewComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this SelectCharacterViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this SelectCharacterViewComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }

        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}
