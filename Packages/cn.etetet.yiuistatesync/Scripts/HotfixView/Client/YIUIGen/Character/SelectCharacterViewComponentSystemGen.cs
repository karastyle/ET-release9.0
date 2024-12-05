using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [FriendOf(typeof(YIUIChild))]
    [FriendOf(typeof(YIUIWindowComponent))]
    [FriendOf(typeof(YIUIViewComponent))]
    [EntitySystemOf(typeof(SelectCharacterViewComponent))]
    public static partial class SelectCharacterViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this SelectCharacterViewComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this SelectCharacterViewComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this SelectCharacterViewComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIView = self.UIBase.GetComponent<YIUIViewComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIView.ViewWindowType = EViewWindowType.View;
            self.UIView.StackOption = EViewStackOption.VisibleTween;

            self.u_ComRoleList = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.LoopHorizontalScrollRect>("u_ComRoleList");
            self.u_EventReady = self.UIBase.EventTable.FindEvent<UITaskEventP0>("u_EventReady");
            self.u_EventReadyHandle = self.u_EventReady.Add(self,SelectCharacterViewComponent.OnEventReadyInvoke);
            self.u_EventBack = self.UIBase.EventTable.FindEvent<UITaskEventP0>("u_EventBack");
            self.u_EventBackHandle = self.u_EventBack.Add(self,SelectCharacterViewComponent.OnEventBackInvoke);

        }
    }
}
