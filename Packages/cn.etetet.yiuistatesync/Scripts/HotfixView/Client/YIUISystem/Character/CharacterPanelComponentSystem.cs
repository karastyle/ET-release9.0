﻿using System;
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
    [FriendOf(typeof(CharacterPanelComponent))]
    public static partial class CharacterPanelComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this CharacterPanelComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this CharacterPanelComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this CharacterPanelComponent self)
        {
            //请求角色列表
            int errorCode = await LoginHelper.GetRoleList(self.Root());
            if (errorCode != ErrorCode.ERR_Success)
            {
                //弹窗提示获取角色列表失败
                TipsHelper.OpenSync<TipsTextViewComponent>("获取角色列表失败");
                return false;
            }
            
            RoleUnitModel roleUnitModel = self.Root().GetComponent<RoleUnitModel>();
            List<EntityRef<RoleUnit>> roleInfos = roleUnitModel.GetRoleList();
            if (roleInfos.Count > 0)
            {
                //选角界面
                await self.UIPanel.OpenViewAsync<SelectCharacterViewComponent>();
            }
            else
            {
                //创角界面
                await self.UIPanel.OpenViewAsync<CreateCharacterViewComponent>();
            }
            return true;
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this CharacterPanelComponent self, SwitchSelectCharacterEvent message)
        {
            //选角界面
            await self.UIPanel.OpenViewAsync<SelectCharacterViewComponent>();
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this CharacterPanelComponent self, SwitchCreateCharacterEvent message)
        {
            //创角界面
            await self.UIPanel.OpenViewAsync<CreateCharacterViewComponent>();
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this CharacterPanelComponent self, CloseCharacterEvent message)
        {
            //创角界面
            await self.UIPanel.CloseAsync();
        }
        

        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}
