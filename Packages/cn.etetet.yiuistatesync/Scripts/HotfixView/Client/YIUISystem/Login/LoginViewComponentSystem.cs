using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.1
    /// Desc
    /// </summary>
    [FriendOf(typeof(LoginViewComponent))]
    public static partial class LoginViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this LoginViewComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this LoginViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this LoginViewComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }

        #region YIUIEvent开始
        
        [YIUIInvoke(LoginViewComponent.OnEventLoginInvoke)]
        private static async ETTask OnEventLoginInvoke(this LoginViewComponent self)
        {
            Log.Info($"登录");
            await self.DynamicEvent(new LoginGameEvent(self.u_ComAccount.text, self.u_ComPassword.text));
            
            await ETTask.CompletedTask;
        }
        #endregion YIUIEvent结束
    }
}
