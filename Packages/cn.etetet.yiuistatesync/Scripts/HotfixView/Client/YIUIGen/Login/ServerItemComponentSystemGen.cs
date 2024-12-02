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
    [EntitySystemOf(typeof(ServerItemComponent))]
    public static partial class ServerItemComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ServerItemComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this ServerItemComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this ServerItemComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();

            self.u_DataServerName = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueString>("u_DataServerName");
            self.u_EventSelect = self.UIBase.EventTable.FindEvent<UIEventP0>("u_EventSelect");
            self.u_EventSelectHandle = self.u_EventSelect.Add(self,ServerItemComponent.OnEventSelectInvoke);

        }
    }
}
