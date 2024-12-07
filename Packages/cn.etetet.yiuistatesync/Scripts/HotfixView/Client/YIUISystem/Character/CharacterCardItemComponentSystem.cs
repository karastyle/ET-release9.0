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

        public static void ResetItem(this CharacterCardItemComponent self, EntityRef<RoleUnit> data)
        {
            self.data = data;
            RoleUnit roleUnit = data;
            self.u_DataCardState.SetValue((int)roleUnit.ShowType);
            if (roleUnit.ShowType == (int)RoleShowType.Empty)
            {
            }
            else
            {
                //角色信息
                self.u_DataName.SetValue(roleUnit.Name);
                HeroConfigCategory.Instance.GetAll().TryGetValue(roleUnit.HeroId, out HeroConfig heroConfig);
                if (heroConfig != null)
                {
                    Log.Info("hero career: " + heroConfig.Name);
                    self.u_DataCareer.SetValue(heroConfig.Name);
                }
                else
                {
                    Log.Error("heroConfig is null" + roleUnit.HeroId);
                }
            }
        }

        public static void SelectItem(this CharacterCardItemComponent self, bool select)
        {
            self.u_DataSelect.SetValue(select);
        }

        private static async ETTask DeleteRole(this CharacterCardItemComponent self)
        {
            RoleUnit data = self.data;
            int errorCode = await LoginHelper.DeleteRole(self.Root(), data.Id);
            if (errorCode != ErrorCode.ERR_Success)
            {
                //删除角色失败
                TipsHelper.OpenSync<TipsTextViewComponent>("删除角色失败");
                return;
            }

            TipsHelper.OpenSync<TipsTextViewComponent>("删除角色成功");

            //发出事件
            await self.DynamicEvent(new DeleteRoleEvent());
        }

        #region YIUIEvent开始

        [YIUIInvoke(CharacterCardItemComponent.OnEventSelectInvoke)]
        private static void OnEventSelectInvoke(this CharacterCardItemComponent self)
        {
            Log.Info("CardItem click");
            if (self.u_DataCardState.GetValue() == (int)RoleShowType.Empty)
            {
                //创建角色
                self.DynamicEvent(new SwitchCreateCharacterEvent()).NoContext();
            }
        }

        [YIUIInvoke(CharacterCardItemComponent.OnEventDeleteInvoke)]
        private static void OnEventDeleteInvoke(this CharacterCardItemComponent self)
        {
            Log.Info("Delete role " + self.u_DataName);
            //删除角色
            self.DeleteRole().NoContext();
        }

        #endregion YIUIEvent结束
    }
}