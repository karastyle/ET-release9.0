using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{

    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [YIUI(EUICodeType.Common)]
    [ComponentOf(typeof(YIUIChild))]
    public partial class CharacterCardItemComponent : Entity, IDestroy, IAwake, IYIUIBind, IYIUIInitialize
    {
        public const string PkgName = "Character";
        public const string ResName = "CharacterCardItem";

        public EntityRef<YIUIChild> u_UIBase;
        public YIUIChild UIBase => u_UIBase;
        public YIUIFramework.UIDataValueInt u_DataCardState;
        public YIUIFramework.UIDataValueString u_DataCareer;
        public YIUIFramework.UIDataValueString u_DataName;
        public YIUIFramework.UIDataValueBool u_DataSelect;
        public UIEventP0 u_EventSelect;
        public UIEventHandleP0 u_EventSelectHandle;
        public const string OnEventSelectInvoke = "CharacterCardItemComponent.OnEventSelectInvoke";
        public UIEventP0 u_EventDelete;
        public UIEventHandleP0 u_EventDeleteHandle;
        public const string OnEventDeleteInvoke = "CharacterCardItemComponent.OnEventDeleteInvoke";

    }
}