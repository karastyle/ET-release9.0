using ET.Server;
using MongoDB.Bson;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene root, string address, string account, string password)
        {
            root.RemoveComponent<ClientSenderComponent>();
            
            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            
            
            NetClient2Main_Login response = await clientSenderComponent.LoginAsync(address, account, password);
            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"登录失败，response Error: {response.Error}");
                return response.Error;
            }
            Log.Debug("登录成功");
            string Token = response.Token;
            //记录token
            root.GetComponent<PlayerComponent>().Token = Token;
            root.GetComponent<PlayerComponent>().Account = account;

            return response.Error;

            // //获取服务器列表
            // C2R_GetServerInfos c2RGetServerInfos = C2R_GetServerInfos.Create();
            // c2RGetServerInfos.Account = account;
            // c2RGetServerInfos.Token = response.Token;
            // R2C_GetServerInfos r2CGetServerInfos = await clientSenderComponent.Call(c2RGetServerInfos) as R2C_GetServerInfos;
            // if (r2CGetServerInfos.Error != ErrorCode.ERR_Success)
            // {
            //     Log.Error("请求服务器列表失败");
            //     return;
            // }
            //
            // ServerInfoProto serverInfoProto = r2CGetServerInfos.ServerInfosList[0];
            // Log.Debug($"请求服务器列表成功, 区服名称:{serverInfoProto.ServerName} 区服ID:{serverInfoProto.Id}");
            //
            // //获取区服角色列表
            // C2R_GetRoles c2RGetRoles = C2R_GetRoles.Create();
            // c2RGetRoles.Token = Token;
            // c2RGetRoles.Account = account;
            // c2RGetRoles.ServerId = serverInfoProto.Id;
            // R2C_GetRoles r2CGetRoles = await clientSenderComponent.Call(c2RGetRoles) as R2C_GetRoles;
            // if (r2CGetRoles.Error != ErrorCode.ERR_Success)
            // {
            //     Log.Error("请求区服角色列表失败");
            //     return;
            // }


            // RoleInfoProto roleInfoProto = default;
            // if (r2CGetRoles.RoleInfo.Count <= 0)
            // {
            //     //无角色， 创建角色 
            //     C2R_CreateRole c2RCreateRole = C2R_CreateRole.Create();
            //     c2RCreateRole.Token = Token;
            //     c2RCreateRole.Account = account;
            //     c2RCreateRole.ServerId = serverInfoProto.Id;
            //     c2RCreateRole.Name = account;
            //     
            //     R2C_CreateRole r2CCreateRole = await clientSenderComponent.Call(c2RCreateRole) as R2C_CreateRole;
            //     if (r2CCreateRole.Error != ErrorCode.ERR_Success)
            //     {
            //         Log.Error("创建区服角色失败！");
            //         return;
            //     }
            //
            //     roleInfoProto = r2CCreateRole.RoleInfo;
            //     
            //     
            // }
            // else
            // {
            //     roleInfoProto = r2CGetRoles.RoleInfo[0];
            // }
            //
            // //请求获取RealmKey， 这个key就是gate的令牌
            // C2R_GetRealmKey c2RGetRealmKey = C2R_GetRealmKey.Create();
            // c2RGetRealmKey.Token = Token;
            // c2RGetRealmKey.Account = account;
            // c2RGetRealmKey.ServerId = serverInfoProto.Id;
            // R2C_GetRealmKey r2CGetRealmKey = await clientSenderComponent.Call(c2RGetRealmKey) as R2C_GetRealmKey;
            // if (r2CGetRealmKey.Error != ErrorCode.ERR_Success)
            // {
            //     Log.Error("获取RealmKey失败");
            //     return;
            // }
            //
            // //请求游戏角色进入map地图
            // NetClient2Main_LoginGame netClient2MainLoginGame =
            //         await clientSenderComponent.LoginGameAsync(account, r2CGetRealmKey.Key, roleInfoProto.Id, r2CGetRealmKey.Address);
            // if (netClient2MainLoginGame.Error != ErrorCode.ERR_Success)
            // {
            //     Log.Error($"进入游戏失败：{netClient2MainLoginGame.Error}");
            //     return;
            // }
            // Log.Debug("进入游戏成功！！！");
            //
            // await EventSystem.Instance.PublishAsync(root, new LoginFinish());
        }
        
        public static async ETTask<int> GetServerList(Scene root)
        {
            //拿到账号信息
            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
        
            //发送网络消息的组件
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();
        
            //获取服务器列表
            C2R_GetServerInfos c2RGetServerInfos = C2R_GetServerInfos.Create();
            c2RGetServerInfos.Account = playerComponent.Account;
            c2RGetServerInfos.Token = playerComponent.Token;
            R2C_GetServerInfos r2CGetServerInfos = await clientSenderComponent.Call(c2RGetServerInfos) as R2C_GetServerInfos;
            if (r2CGetServerInfos.Error != ErrorCode.ERR_Success)
            {
                Log.Error("请求服务器列表失败");
                return r2CGetServerInfos.Error;
            }

            //存起来
            ServerInfoComponent serverInfoComponentSystem = root.GetComponent<ServerInfoComponent>();
            if (r2CGetServerInfos.ServerInfosList.Count > 0)
            {
                foreach (var serverProto in r2CGetServerInfos.ServerInfosList)
                {
                    ServerInfo serverInfo = serverInfoComponentSystem.AddChildWithId<ServerInfo>(serverProto.Id);
                    serverInfo.FromMessage(serverProto);
                }
            }

            return r2CGetServerInfos.Error;
        }

        //获取角色列表
        public static async ETTask<int> GetRoleList(Scene root)
        {
            //拿到账号信息
            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
        
            //发送网络消息的组件
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();

            //获取区服角色列表
            C2R_GetRoles c2RGetRoles = C2R_GetRoles.Create();
            c2RGetRoles.Token = playerComponent.Token;
            c2RGetRoles.Account = playerComponent.Account;
            c2RGetRoles.ServerId = playerComponent.ServerId;
            R2C_GetRoles r2CGetRoles = await clientSenderComponent.Call(c2RGetRoles) as R2C_GetRoles;
            if (r2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Error("请求区服角色列表失败");
                return r2CGetRoles.Error;
            }
            
            //存起来
            RoleInfoComponent roleInfoComponent = root.GetComponent<RoleInfoComponent>();
            //这里是覆盖，要先清空
            roleInfoComponent.RemoveAll();
            if (r2CGetRoles.RoleInfo.Count > 0)
            {
                foreach (var roleProto in r2CGetRoles.RoleInfo)
                {
                    RoleInfo roleInfo = roleInfoComponent.AddChildWithId<RoleInfo>(roleProto.Id);
                    roleInfo.FromMessage(roleProto);
                }
            }
            
            return r2CGetRoles.Error;
        }
        
        //创建角色
        public static async ETTask<int> CreateRole(Scene root, int heroId, string name)
        {
            //拿到账号信息
            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
        
            //发送网络消息的组件
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();

            //无角色， 创建角色 
            C2R_CreateRole c2RCreateRole = C2R_CreateRole.Create();
            c2RCreateRole.Token = playerComponent.Token;
            c2RCreateRole.Account = playerComponent.Account;
            c2RCreateRole.ServerId = playerComponent.ServerId;
            c2RCreateRole.Name = name;
            c2RCreateRole.HeroId = heroId;
            
            R2C_CreateRole r2CCreateRole = await clientSenderComponent.Call(c2RCreateRole) as R2C_CreateRole;
            if (r2CCreateRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error("创建角色失败！");
                return r2CCreateRole.Error;
            }
            
            return r2CCreateRole.Error;
        }
        
        //删除角色
        public static async ETTask<int> DeleteRole(Scene root, long roleId)
        {
            //拿到账号信息
            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
        
            //发送网络消息的组件
            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();

            //无角色， 创建角色 
            C2R_DeleteRole c2RDeleteRole = C2R_DeleteRole.Create();
            c2RDeleteRole.Token = playerComponent.Token;
            c2RDeleteRole.Account = playerComponent.Account;
            c2RDeleteRole.ServerId = playerComponent.ServerId;
            c2RDeleteRole.RoleInfoId = roleId;
            
            R2C_DeleteRole r2CDeleteRole = await clientSenderComponent.Call(c2RDeleteRole) as R2C_DeleteRole;
            if (r2CDeleteRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error("删除角色失败！");
                return r2CDeleteRole.Error;
            }
            
            return r2CDeleteRole.Error;
        }
    }
    
   
}