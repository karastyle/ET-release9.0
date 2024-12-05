using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.3
    /// Desc
    /// </summary>
    [FriendOf(typeof(CharacterCardItemComponent))]
    public static partial class CharacterCardItemComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this CharacterCardItemComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this CharacterCardItemComponent self)
        {
        }

        public static void ResetItem(this CharacterCardItemComponent self, EntityRef<RoleInfo> data)
        {
            self.data = data;
            RoleInfo roleInfo = data;
            self.u_DataCardState.SetValue((int)roleInfo.ShowType);
            if (roleInfo.ShowType == (int)RoleShowType.Empty)
            {
            }
            else
            {
                //角色信息
                self.u_DataName.SetValue(roleInfo.Name);
                HeroConfigCategory.Instance.GetAll().TryGetValue(roleInfo.HeroId, out HeroConfig heroConfig);
                if (heroConfig != null)
                {
                    Log.Debug("hero career: " + heroConfig.Name);
                    self.u_DataCareer.SetValue(heroConfig.Name);
                }
                else
                {
                    Log.Error("heroConfig is null" + roleInfo.HeroId);
                }
            }
        }

        public static void SelectItem(this CharacterCardItemComponent self, bool select)
        {
            self.u_DataSelect.SetValue(select);
        }

        private static async ETTask DeleteRole(this CharacterCardItemComponent self)
        {
            RoleInfo data = self.data;
            int errorCode = await LoginHelper.DeleteRole(self.Root(), data.Id);
            if (errorCode != ErrorCode.ERR_Success)
            {
                //删除角色失败
                TipsHelper.OpenSync<TipsTextViewComponent>("删除角色失败");
                return;
            }
            
            TipsHelper.OpenSync<TipsTextViewComponent>("删除角色成功");
        }

        #region YIUIEvent开始

        [YIUIInvoke(CharacterCardItemComponent.OnEventSelectInvoke)]
        private static void OnEventSelectInvoke(this CharacterCardItemComponent self)
        {
            Log.Debug("CardItem click");
            if (self.u_DataCardState.GetValue() == (int)RoleShowType.Empty)
            {
                //创建角色
                YIUIMgrComponent.Inst.GetPanel<LoginPanelComponent>().UIPanel.OpenViewAsync<CreateCharacterViewComponent>().NoContext();
            }
        }

        [YIUIInvoke(CharacterCardItemComponent.OnEventDeleteInvoke)]
        private static void OnEventDeleteInvoke(this CharacterCardItemComponent self)
        {
            //删除角色
            self.DeleteRole().NoContext();
        }

        #endregion YIUIEvent结束
    }
}