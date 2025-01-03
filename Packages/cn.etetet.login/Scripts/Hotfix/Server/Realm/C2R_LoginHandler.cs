﻿using System;
using System.Collections.Generic;
using System.Net;


namespace ET.Server
{
	[MessageSessionHandler(SceneType.Realm)]
	public class C2R_LoginHandler : MessageSessionHandler<C2R_Login, R2C_Login>
	{
		protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
		{
			//验证账号密码， 使用db数据库
			if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
			{
				//没填
				response.Error = ErrorCode.ERR_LoginInfoEmpty;
				CloseSession(session).NoContext();
				return;
			}

			using (await session.Root().GetComponent<CoroutineLockComponent>()
					       .Wait(CoroutineLockType.LoginAccount, request.Account.GetLongHashCode()))
			{
				// DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
				// List<AccountInfo> accountInfos = await dbComponent.Query<AccountInfo>(accountInfo => accountInfo.Account == request.Account);
				// if (accountInfos.Count <= 0)
				// {
				// 	AccountInfosComponent accountInfosComponent =
				// 			session.GetComponent<AccountInfosComponent>() ?? session.AddComponent<AccountInfosComponent>();
				// 	AccountInfo accountInfo = accountInfosComponent.AddChild<AccountInfo>();
				// 	accountInfo.Account = request.Account;
				// 	accountInfo.Password = request.Password;
				// 	await dbComponent.Save(accountInfo);
				// }
				// else
				// {
				// 	AccountInfo accountInfo = accountInfos[0];
				// 	if (accountInfo.Password != request.Password)
				// 	{
				// 		response.Error = ErrorCode.ERR_LoginPasswordError;
				// 		CloseSession(session).NoContext();
				// 		return;
				// 	}
				// }
			}
			
			
			const int UserZone = 3; // 这里一般会有创角，选择区服，demo就不做这个操作了，直接放在3区
			// 随机分配一个Gate
			StartSceneConfig config = RealmGateAddressHelper.GetGate(UserZone, request.Account);
			Log.Debug($"gate address: {config}");
			
			// 向gate请求一个key,客户端可以拿着这个key连接gate
			R2G_GetLoginKey r2GGetLoginKey = R2G_GetLoginKey.Create();
			r2GGetLoginKey.Account = request.Account;
			G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey) await session.Fiber().Root.GetComponent<MessageSender>().Call(
				config.ActorId, r2GGetLoginKey);

			response.Address = config.InnerIPPort.ToString();
			response.Key = g2RGetLoginKey.Key;
			response.GateId = g2RGetLoginKey.GateId;
			
			CloseSession(session).NoContext();
		}

		private async ETTask CloseSession(Session session)
		{
			await session.Root().GetComponent<TimerComponent>().WaitAsync(1000);
			session.Dispose();
		}
	}
}
