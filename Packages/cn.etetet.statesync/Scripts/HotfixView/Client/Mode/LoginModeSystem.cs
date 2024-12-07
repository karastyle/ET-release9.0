using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(LoginMode))]
    [FriendOf(typeof(LoginMode))]
    public static partial class  LoginModeSystem
    {
        [EntitySystem]
        private static async ETTask DynamicEvent(this LoginMode self, EnterPlayModeEvent message)
        {
            //Enter Mode
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }

            //打开登录界面
            await YIUIMgrComponent.Inst.Root.OpenPanelAsync<LoginPanelComponent>();
            
            //LoadingRoot隐藏， 这个是用来避免启动的时候看不到东西，就加了一个背景
            GameObject.Find("StartDefaultRoot").gameObject.SetActive(false);
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this LoginMode self, ExitPlayModeEvent message)
        {
            //Exit Mode
            await ETTask.CompletedTask;
            if (message.playMode != self.playMode)
            {
                return;
            }

      
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this LoginMode self, LoginGameEvent message)
        {
            //登录
            GlobalComponent globalComponent = self.Root().GetComponent<GlobalComponent>();
            //对于异步逻辑，如果没有调用的地方也需要处理逻辑，那么可以抛事件。 像这里是直接调用的，可以用返回值来处理逻辑。
            int errorCode = await LoginHelper.Login(self.Root(),
                globalComponent.GlobalConfig.Address,
                message.account,
                message.password);

            if (errorCode == ErrorCode.ERR_Success)
            {
                //登录成功，  进入选服
                await self.DynamicEvent(new SwitchSelectServerEvent());
            }
            else
            {
                //登录失败，弹窗提示
                TipsHelper.OpenSync<TipsTextViewComponent>("登录失败");
            }
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this LoginMode self, SelectServerEvent message)
        {
            //选服
            
        }
        
        [EntitySystem]
        private static void Awake(this ET.Client.LoginMode self)
        {
        }

        [EntitySystem]
        private static void Update(this ET.Client.LoginMode self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ET.Client.LoginMode self)
        {
        }
    }
}

