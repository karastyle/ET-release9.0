using System;
using UnityEngine;
using YIUIFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace ET.Client
{
    /// <summary>
    /// Author  Kara
    /// Date    2024.12.1
    /// Desc
    /// </summary>
    [FriendOf(typeof(SelectServerViewComponent))]
    public static partial class SelectServerViewComponentSystem
    {
        
        [EntitySystem]
        private static void YIUIInitialize(this SelectServerViewComponent self)
        {
            self.ServerDataList         = new();
            self.m_ServerLoop       = self.AddChild<YIUILoopScrollChild, LoopScrollRect, Type, string>(self.u_ComServerList, typeof(ServerItemComponent), "u_EventSelect");
        }

        [EntitySystem]
        private static void Destroy(this SelectServerViewComponent self)
        {
        }

        [EntitySystem]
        private static async ETTask<bool> YIUIOpen(this SelectServerViewComponent self)
        {
            //拉取服务器列表
            int errorCode = await LoginHelper.GetServerList(self.Root());
            if (errorCode != ErrorCode.ERR_Success)
            {
                //弹窗提示获取服务器列表失败

                return false;
            }
            
            ServerInfoModel serverInfoModel = self.Root().GetComponent<ServerInfoModel>();
            self.ServerDataList = serverInfoModel.GetServerList();
            
            //设置滚动列表
            self.ServereLoop.ClearSelect();
            self.ServereLoop.SetDataRefresh(self.ServerDataList, 0).NoContext();;
            
            await ETTask.CompletedTask;
            return true;
        }

        [EntitySystem]
        private static void YIUILoopRenderer(this SelectServerViewComponent self, ServerItemComponent item, EntityRef<ServerInfo> data, int index, bool select)
        {
            item.ResetItem(data);
            item.SelectItem(select);
            if (select)
            {
                ServerInfo server = data;
                self.OnSelectServer(server);
            }
        }

        [EntitySystem]
        private static void YIUILoopOnClick(this SelectServerViewComponent self, ServerItemComponent item, EntityRef<ServerInfo> data, int index, bool select)
        {
            item.SelectItem(select);
            if (select)
            {
                ServerInfo server = data;
                self.OnSelectServer(server);
                self.u_DataShowServer.SetValue(false);
            }
        }

        //选服回调
        private static void OnSelectServer(this SelectServerViewComponent self, ServerInfo server)
        {
            self.u_DataCurServerName.SetValue(server.ServerName);
            AccountModel serverInfoModelSystem = self.Root().GetComponent<AccountModel>();
            serverInfoModelSystem.ServerId = server.ServerId;
        }

        #region YIUIEvent开始
        
        [YIUIInvoke(SelectServerViewComponent.OnEventStartInvoke)]
        private static async ETTask OnEventStartInvoke(this SelectServerViewComponent self)
        {
            await YIUIMgrComponent.Inst.Root.OpenPanelAsync<CharacterPanelComponent>();
            await ETTask.CompletedTask;
        }
        #endregion YIUIEvent结束
    }
}
