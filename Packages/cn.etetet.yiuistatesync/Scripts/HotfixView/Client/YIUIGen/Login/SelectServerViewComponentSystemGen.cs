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
    [EntitySystemOf(typeof(SelectServerViewComponent))]
    public static partial class SelectServerViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this SelectServerViewComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this SelectServerViewComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this SelectServerViewComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();
            self.u_UIWindow = self.UIBase.GetComponent<YIUIWindowComponent>();
            self.u_UIView = self.UIBase.GetComponent<YIUIViewComponent>();
            self.UIWindow.WindowOption = EWindowOption.None;
            self.UIView.ViewWindowType = EViewWindowType.View;
            self.UIView.StackOption = EViewStackOption.VisibleTween;

            self.u_ComServerList = self.UIBase.ComponentTable.FindComponent<UnityEngine.UI.LoopVerticalScrollRect>("u_ComServerList");
            self.u_DataShowServer = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueBool>("u_DataShowServer");
            self.u_DataCurServerName = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueString>("u_DataCurServerName");
            self.u_EventStart = self.UIBase.EventTable.FindEvent<UITaskEventP0>("u_EventStart");
            self.u_EventStartHandle = self.u_EventStart.Add(self,SelectServerViewComponent.OnEventStartInvoke);

        }
    }
}
