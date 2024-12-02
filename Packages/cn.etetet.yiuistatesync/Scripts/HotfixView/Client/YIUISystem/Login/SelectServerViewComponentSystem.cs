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
            
            ServerInfoComponent serverInfoComponentSystem = self.Root().GetComponent<ServerInfoComponent>();
            self.ServerDataList = serverInfoComponentSystem.GetServerList();
            
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
                self.u_DataCurServerName.SetValue(server.ServerName);
            }
        }

        [EntitySystem]
        private static void YIUILoopOnClick(this SelectServerViewComponent self, ServerItemComponent item, EntityRef<ServerInfo> data, int index, bool select)
        {
            item.SelectItem(select);
            if (select)
            {
                ServerInfo server = data;
                self.u_DataCurServerName.SetValue(server.ServerName);
                self.u_DataShowServer.SetValue(false);
            }
        }

        #region YIUIEvent开始
        #endregion YIUIEvent结束
    }
}
