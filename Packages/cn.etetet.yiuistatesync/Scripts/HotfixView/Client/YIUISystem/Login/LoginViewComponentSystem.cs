using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.1
    /// Desc
    /// </summary>
    [FriendOf(typeof(LoginViewComponent))]
    public static partial class LoginViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this LoginViewComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this LoginViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this LoginViewComponent self)
        {
            await ETTask.CompletedTask;
            return true;
        }

        #region YIUIEvent开始
        
        [YIUIInvoke(LoginViewComponent.OnEventLoginInvoke)]
        private static async ETTask OnEventLoginInvoke(this LoginViewComponent self)
        {
            Log.Info($"登录");
            GlobalComponent globalComponent = self.Root().GetComponent<GlobalComponent>();
            //对于异步逻辑，如果没有调用的地方也需要处理逻辑，那么可以抛事件。 像这里是直接调用的，可以用返回值来处理逻辑。
            int errorCode = await LoginHelper.Login(self.Root(),
                globalComponent.GlobalConfig.Address,
                self.u_ComAccount.text,
                self.u_ComPassword.text);

            if (errorCode == ErrorCode.ERR_Success)
            {
                //登录成功，  进入选服
                await YIUIMgrComponent.Inst.GetPanel<LoginPanelComponent>().UIPanel.OpenViewAsync<SelectServerViewComponent>();
            }
            else
            {
                //登录失败，弹窗提示
            }
        }
        #endregion YIUIEvent结束
    }
}
