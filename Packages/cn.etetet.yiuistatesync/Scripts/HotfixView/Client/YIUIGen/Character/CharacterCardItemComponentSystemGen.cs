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
    [EntitySystemOf(typeof(CharacterCardItemComponent))]
    public static partial class CharacterCardItemComponentSystem
    {
        [EntitySystem]
        private static void Awake(this CharacterCardItemComponent self)
        {
        }

        [EntitySystem]
        private static void YIUIBind(this CharacterCardItemComponent self)
        {
            self.UIBind();
        }

        private static void UIBind(this CharacterCardItemComponent self)
        {
            self.u_UIBase = self.GetParent<YIUIChild>();

            self.u_DataCardState = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueInt>("u_DataCardState");
            self.u_DataCareer = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueString>("u_DataCareer");
            self.u_DataName = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueString>("u_DataName");
            self.u_DataSelect = self.UIBase.DataTable.FindDataValue<YIUIFramework.UIDataValueBool>("u_DataSelect");
            self.u_EventSelect = self.UIBase.EventTable.FindEvent<UIEventP0>("u_EventSelect");
            self.u_EventSelectHandle = self.u_EventSelect.Add(self,CharacterCardItemComponent.OnEventSelectInvoke);
            self.u_EventDelete = self.UIBase.EventTable.FindEvent<UIEventP0>("u_EventDelete");
            self.u_EventDeleteHandle = self.u_EventDelete.Add(self,CharacterCardItemComponent.OnEventDeleteInvoke);

        }
    }
}
