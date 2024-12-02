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
    [EntitySystemOf(typeof(CreateCharacterViewComponent))]
    public static partial class CreateCharacterViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this CreateCharacterViewComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this CreateCharacterViewComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this CreateCharacterViewComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIView = self.UIBase.GetComponent<YIUIViewComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIView.ViewWindowType = EViewWindowType.View;
            self.UIView.StackOption = EViewStackOption.VisibleTween;

            self.u_DataCareer = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueInt>("u_DataCareer");
            self.u_EventClickSelect = self.UIBase.EventTable.FindEvent<UITaskEventP0>("u_EventClickSelect");
            self.u_EventClickSelectHandle = self.u_EventClickSelect.Add(self,CreateCharacterViewComponent.OnEventClickSelectInvoke);

        }
    }
}
