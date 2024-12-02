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
    [FriendOf(typeof(ServerItemComponent))]
    public static partial class ServerItemComponentSystem
    {
        [EntitySystem]
        private static void YIUIInitialize(this ServerItemComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ServerItemComponent self)
        {
        }

        public static void ResetItem(this ServerItemComponent self, EntityRef<ServerInfo> data)
        {
            ServerInfo server = data;
            self.u_DataServerName.SetValue(server.ServerName);
        }

        public static void SelectItem(this ServerItemComponent self, bool select)
        {
            
        }
        
        #region YIUIEvent开始
        
        
        [YIUIInvoke(ServerItemComponent.OnEventSelectInvoke)]
        private static void OnEventSelectInvoke(this ServerItemComponent self)
        {

        }
        #endregion YIUIEvent结束
    }
}
