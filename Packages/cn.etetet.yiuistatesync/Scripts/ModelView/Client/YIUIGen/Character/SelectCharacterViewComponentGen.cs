﻿using System;
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
    public partial class SelectCharacterViewComponent : Entity, IDestroy, IAwake, IYIUIBind, IYIUIInitialize, IYIUIOpen
    {
        public const string PkgName = "Character";
        public const string ResName = "SelectCharacterView";

        public EntityRef<YIUIChild> u_UIBase;
        public YIUIChild UIBase => u_UIBase;
        public EntityRef<YIUIWindowComponent> u_UIWindow;
        public YIUIWindowComponent UIWindow => u_UIWindow;
        public EntityRef<YIUIViewComponent> u_UIView;
        public YIUIViewComponent UIView => u_UIView;
        public UnityEngine.UI.LoopHorizontalScrollRect u_ComRoleList;
        public UITaskEventP0 u_EventReady;
        public UITaskEventHandleP0 u_EventReadyHandle;
        public const string OnEventReadyInvoke = "SelectCharacterViewComponent.OnEventReadyInvoke";
        public UITaskEventP0 u_EventBack;
        public UITaskEventHandleP0 u_EventBackHandle;
        public const string OnEventBackInvoke = "SelectCharacterViewComponent.OnEventBackInvoke";

    }
}