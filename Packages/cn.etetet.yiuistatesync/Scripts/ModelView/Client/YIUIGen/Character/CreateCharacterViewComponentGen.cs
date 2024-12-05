using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{

    /// <summary>
    /// 由YIUI工具自动创建 请勿修改
    /// </summary>
    [YIUI(EUICodeType.View)]
    [ComponentOf(typeof(YIUIChild))]
    public partial class CreateCharacterViewComponent : Entity, IDestroy, IAwake, IYIUIBind, IYIUIInitialize, IYIUIOpen
    {
        public const string PkgName = "Character";
        public const string ResName = "CreateCharacterView";

        public EntityRef<YIUIChild> u_UIBase;
        public YIUIChild UIBase => u_UIBase;
        public EntityRef<YIUIWindowComponent> u_UIWindow;
        public YIUIWindowComponent UIWindow => u_UIWindow;
        public EntityRef<YIUIViewComponent> u_UIView;
        public YIUIViewComponent UIView => u_UIView;
        public YIUIFramework.UI3DDisplay u_ComCharacter;
        public UnityEngine.UI.InputField u_ComHeroName;
        public YIUIFramework.UIDataValueInt u_DataCareer;
        public UITaskEventP0 u_EventClickSelect;
        public UITaskEventHandleP0 u_EventClickSelectHandle;
        public const string OnEventClickSelectInvoke = "CreateCharacterViewComponent.OnEventClickSelectInvoke";
        public UIEventP0 u_EventClickHero;
        public UIEventHandleP0 u_EventClickHeroHandle;
        public const string OnEventClickHeroInvoke = "CreateCharacterViewComponent.OnEventClickHeroInvoke";
        public UITaskEventP0 u_EventClose;
        public UITaskEventHandleP0 u_EventCloseHandle;
        public const string OnEventCloseInvoke = "CreateCharacterViewComponent.OnEventCloseInvoke";

    }
}