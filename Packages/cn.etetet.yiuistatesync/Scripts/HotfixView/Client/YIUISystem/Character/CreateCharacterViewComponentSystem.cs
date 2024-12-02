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
    [FriendOf(typeof(CreateCharacterViewComponent))]
    public static partial class CreateCharacterViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this CreateCharacterViewComponent self)
        {
            self.u_DataCareer.AddValueChangeAction(CareerChangeAction);
        }

        private static void CareerChangeAction(int arg1, int arg2)
        {
            
        }
        
        [EntitySystem]
        private static void Destroy(this CreateCharacterViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this CreateCharacterViewComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }

        #region YIUIEvent开始
        
        [YIUIInvoke(CreateCharacterViewComponent.OnEventClickSelectInvoke)]
        private static async ETTask OnEventClickSelectInvoke(this CreateCharacterViewComponent self)
        {
            
            await ETTask.CompletedTask;
        }
        #endregion YIUIEvent结束
    }
}
