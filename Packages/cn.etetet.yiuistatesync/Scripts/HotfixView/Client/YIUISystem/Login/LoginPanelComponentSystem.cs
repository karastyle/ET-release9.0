using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    [FriendOf(typeof(LoginPanelComponent))]
    public static partial class LoginPanelComponentSystem
    {
        
        [EntitySystem]
        private static void YIUIInitialize(this LoginPanelComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this LoginPanelComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this LoginPanelComponent self)
        {
            await self.UIPanel.OpenViewAsync<LoginViewComponent>();
            return true;
        }

        [EntitySystem]
        private static async ETTask DynamicEvent(this LoginPanelComponent self, SwitchSelectServerEvent message)
        {
            await self.UIPanel.OpenViewAsync<SelectServerViewComponent>();
        }
    }
}