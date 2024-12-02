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
    [EntitySystemOf(typeof(LoginViewComponent))]
    public static partial class LoginViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this LoginViewComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this LoginViewComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this LoginViewComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIView = self.UIBase.GetComponent<YIUIViewComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIView.ViewWindowType = EViewWindowType.View;
            self.UIView.StackOption = EViewStackOption.VisibleTween;

            self.u_ComAccount = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.InputField>("u_ComAccount");
            self.u_ComPassword = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.InputField>("u_ComPassword");
            self.u_EventLogin = self.UIBase.EventTable.FindEvent<UITaskEventP0>("u_EventLogin");
            self.u_EventLoginHandle = self.u_EventLogin.Add(self,LoginViewComponent.OnEventLoginInvoke);

        }
    }
}
