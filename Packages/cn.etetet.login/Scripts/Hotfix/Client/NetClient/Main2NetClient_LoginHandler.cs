﻿using System;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2NetClient_LoginHandler: MessageHandler<Scene, Main2NetClient_Login, NetClient2Main_Login>
    {
        protected override async ETTask Run(Scene root, Main2NetClient_Login request, NetClient2Main_Login response)
        {
            string account = request.Account;
            string password = request.Password;
            // 创建一个ETModel层的Session
            root.RemoveComponent<RouterAddressComponent>();
            // 获取路由跟realmDispatcher地址
            RouterAddressComponent routerAddressComponent =
                    root.AddComponent<RouterAddressComponent, string>(request.Address);
            await routerAddressComponent.Init();
#if UNITY_WEBGL
            root.AddComponent<NetComponent, IKcpTransport>(new WebSocketTransport(routerAddressComponent.AddressFamily));
#else
            root.AddComponent<NetComponent, IKcpTransport>(new UdpTransport(routerAddressComponent.AddressFamily));
#endif
            root.GetComponent<FiberParentComponent>().ParentFiberId = request.OwnerFiberId;

            NetComponent netComponent = root.GetComponent<NetComponent>();
            
            IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);


            R2C_LoginAccount r2CLogin;
            //连接realm 服务器
            Session session = await netComponent.CreateRouterSession(realmAddress, account, password);
                    
            C2R_LoginAccount c2RLogin = C2R_LoginAccount.Create();
            c2RLogin.Account = account;
            c2RLogin.Password = password;
            r2CLogin = (R2C_LoginAccount)await session.Call(c2RLogin);

            if (r2CLogin.Error != ErrorCode.ERR_Success)
            {
                //失败，释放session
                session?.Dispose();
            }
            else
            {
                //记录realm session    后续netClient要进入地图，也是通过realm session来请求
                root.AddComponent<SessionComponent>().Session = session;
            }

            response.Token = r2CLogin.Token;
            response.Error = r2CLogin.Error;

            // 创建一个gate Session,并且保存到SessionComponent中
            // Session gateSession = await netComponent.CreateRouterSession(NetworkHelper.ToIPEndPoint(r2CLogin.Address), account, password);
            // gateSession.AddComponent<ClientSessionErrorComponent>();
            // root.AddComponent<SessionComponent>().Session = gateSession;
            // C2G_LoginGate c2GLoginGate = C2G_LoginGate.Create();
            // c2GLoginGate.Key = r2CLogin.Key;
            // c2GLoginGate.GateId = r2CLogin.GateId;
            // G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(c2GLoginGate);


            // Log.Debug("登陆gate成功!");

            // response.PlayerId = g2CLoginGate.PlayerId;
        }
    }
}