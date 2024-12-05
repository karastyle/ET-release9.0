using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.2
    /// Desc
    /// </summary>
    [FriendOf(typeof(CreateCharacterViewComponent))]
    public static partial class CreateCharacterViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this CreateCharacterViewComponent self)
        {
            self.m_Display = self.AddChild<YIUI3DDisplayChild, UI3DDisplay>(self.u_ComCharacter);
        }

        [EntitySystem]
        private static void Destroy(this CreateCharacterViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this CreateCharacterViewComponent self)
        {
            self.u_DataCareer.SetValue(1, true);
            self.OnSelect().NoContext();
            await ETTask.CompletedTask;
            return true;
        }

        private static async ETTask OnSelect(this CreateCharacterViewComponent self)
        {
            //选中职业
            self.CurCareerId = self.u_DataCareer.GetValue();
            
            StartSceneConfigCategory.Instance.GetBySceneType(SceneType.Realm);
            HeroConfigCategory.Instance.GetAll().TryGetValue(self.CurCareerId, out HeroConfig heroConfig);
            if (heroConfig != null)
            {
                //展示3d模型
                await self.Display.ShowAsync(heroConfig.model, null);
            }
            else
            {
                Log.Error("not found model, career id:"+ self.CurCareerId);
            }
            await ETTask.CompletedTask;
        }
        
        #region YIUIEvent开始
        
        //创建角色
        [YIUIInvoke(CreateCharacterViewComponent.OnEventClickSelectInvoke)]
        private static async ETTask OnEventClickSelectInvoke(this CreateCharacterViewComponent self)
        {
            if (self.u_ComHeroName.text == "")
            {
                TipsHelper.OpenSync<TipsTextViewComponent>("请输入名字");
                return;
            }
            
            //请求创建角色
            int errorCode = await LoginHelper.CreateRole(self.Root(), self.CurCareerId, self.u_ComHeroName.text);
            if (errorCode != ErrorCode.ERR_Success)
            {
                //创建角色失败
                TipsHelper.OpenSync<TipsTextViewComponent>("创建角色失败");
                return;
            }
            
            await YIUIMgrComponent.Inst.GetPanel<CharacterPanelComponent>().UIPanel.OpenViewAsync<SelectCharacterViewComponent>();
        }
        
        //选中页签
        [YIUIInvoke(CreateCharacterViewComponent.OnEventClickHeroInvoke)]
        private static void OnEventClickHeroInvoke(this CreateCharacterViewComponent self)
        {
            self.OnSelect().NoContext();
        }
        
        //关闭
        [YIUIInvoke(CreateCharacterViewComponent.OnEventCloseInvoke)]
        private static async ETTask OnEventCloseInvoke(this CreateCharacterViewComponent self)
        {
            self.UIView.Close();
            await ETTask.CompletedTask;
        }
        #endregion YIUIEvent结束
    }
}
