using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.2
    /// Desc
    /// </summary>
    [FriendOf(typeof(SelectCharacterViewComponent))]
    public static partial class SelectCharacterViewComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this SelectCharacterViewComponent self)
        {
            self.RoleDataList         = new();
            self.m_RoleLoop       = self.AddChild<YIUILoopScrollChild, LoopScrollRect, Type, string>(self.u_ComRoleList, typeof(CharacterCardItemComponent), "u_EventSelect");
        }

        [EntitySystem]
        private static void Destroy(this SelectCharacterViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this SelectCharacterViewComponent self)
        {
            await self.RefreshRoleList();
            await ETTask.CompletedTask;
            return true;
        }

        private static async ETTask RefreshRoleList(this SelectCharacterViewComponent self)
        {
            //每次打开都要请求角色列表
            int errorCode = await LoginHelper.GetRoleList(self.Root());
            if (errorCode != ErrorCode.ERR_Success)
            {
                //弹窗提示获取角色列表失败
                TipsHelper.OpenSync<TipsTextViewComponent>("获取角色列表失败");
                return;
            }
            
            RoleInfoComponent roleInfoComponent = self.Root().GetComponent<RoleInfoComponent>();
            self.RoleDataList = roleInfoComponent.GetRoleList();
            
            for (int i = self.RoleDataList.Count; i < 5; i++)
            {
                RoleInfo roleInfo = new();
                roleInfo.ShowType = (int)RoleShowType.Empty;
                self.RoleDataList.Add(roleInfo);
            }
            
            //设置滚动列表
            self.RoleLoop.ClearSelect();
            self.RoleLoop.SetDataRefresh(self.RoleDataList, 0).NoContext();;
        }
        
        [EntitySystem]
        private static void YIUILoopRenderer(this SelectCharacterViewComponent self, CharacterCardItemComponent item, EntityRef<RoleInfo> data, int index, bool select)
        {
            item.ResetItem(data);
            item.SelectItem(select);
            if (select)
            {
            }
        }

        [EntitySystem]
        private static void YIUILoopOnClick(this SelectCharacterViewComponent self, CharacterCardItemComponent item, EntityRef<RoleInfo> data, int index, bool select)
        {
            item.SelectItem(select);
            if (select)
            {
            }
        }
        
        [EntitySystem]
        private static async ETTask DynamicEvent(this SelectCharacterViewComponent self, DeleteRoleEvent message)
        {
            //删除角色消息， 重新拉去列表刷新一下
            await self.RefreshRoleList();
        }
        
        #region YIUIEvent开始
        
        [YIUIInvoke(SelectCharacterViewComponent.OnEventBackInvoke)]
        private static async ETTask OnEventBackInvoke(this SelectCharacterViewComponent self)
        {
            Log.Info("Close Character View");
            //关闭选角界面
            self.DynamicEvent(new CloseCharacterEvent()).NoContext();
            await ETTask.CompletedTask;
        }
        
        [YIUIInvoke(SelectCharacterViewComponent.OnEventReadyInvoke)]
        private static async ETTask OnEventReadyInvoke(this SelectCharacterViewComponent self)
        {
            
            await ETTask.CompletedTask;
        }
        #endregion YIUIEvent结束
    }
}
