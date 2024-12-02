using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    [MemoryPackable]
    [Message(LoginInner.R2G_GetLoginKey)]
    [ResponseType(nameof(G2R_GetLoginKey))]
    public partial class R2G_GetLoginKey : MessageObject, IRequest
    {
        public static R2G_GetLoginKey Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<R2G_GetLoginKey>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Account { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Account = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.G2R_GetLoginKey)]
    public partial class G2R_GetLoginKey : MessageObject, IResponse
    {
        public static G2R_GetLoginKey Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<G2R_GetLoginKey>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public long Key { get; set; }

        [MemoryPackOrder(4)]
        public long GateId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.Key = default;
            this.GateId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.G2M_SessionDisconnect)]
    public partial class G2M_SessionDisconnect : MessageObject, ILocationMessage
    {
        public static G2M_SessionDisconnect Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<G2M_SessionDisconnect>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.R2L_LoginAccountRequest)]
    [ResponseType(nameof(L2R_LoginAccountRequest))]
    public partial class R2L_LoginAccountRequest : MessageObject, IRequest
    {
        public static R2L_LoginAccountRequest Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<R2L_LoginAccountRequest>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.L2R_LoginAccountRequest)]
    public partial class L2R_LoginAccountRequest : MessageObject, IResponse
    {
        public static L2R_LoginAccountRequest Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<L2R_LoginAccountRequest>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.L2G_DisconnectGateUnit)]
    [ResponseType(nameof(G2L_DisconnectGateUnit))]
    public partial class L2G_DisconnectGateUnit : MessageObject, IRequest
    {
        public static L2G_DisconnectGateUnit Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<L2G_DisconnectGateUnit>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.G2L_DisconnectGateUnit)]
    public partial class G2L_DisconnectGateUnit : MessageObject, IResponse
    {
        public static G2L_DisconnectGateUnit Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<G2L_DisconnectGateUnit>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.G2L_AddLoginRecord)]
    [ResponseType(nameof(L2G_AddLoginRecord))]
    public partial class G2L_AddLoginRecord : MessageObject, IRequest
    {
        public static G2L_AddLoginRecord Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<G2L_AddLoginRecord>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        [MemoryPackOrder(2)]
        public int ServerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;
            this.ServerId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.L2G_AddLoginRecord)]
    public partial class L2G_AddLoginRecord : MessageObject, IResponse
    {
        public static L2G_AddLoginRecord Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<L2G_AddLoginRecord>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.G2L_RemoveLoginRecord)]
    [ResponseType(nameof(L2G_RemoveloginRecord))]
    public partial class G2L_RemoveLoginRecord : MessageObject, IRequest
    {
        public static G2L_RemoveLoginRecord Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<G2L_RemoveLoginRecord>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string AccountName { get; set; }

        [MemoryPackOrder(2)]
        public int ServerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.AccountName = default;
            this.ServerId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.L2G_RemoveloginRecord)]
    public partial class L2G_RemoveloginRecord : MessageObject, IResponse
    {
        public static L2G_RemoveloginRecord Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<L2G_RemoveloginRecord>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.G2M_RequestExitGame)]
    [ResponseType(nameof(M2G_RequestExitGame))]
    public partial class G2M_RequestExitGame : MessageObject, ILocationRequest
    {
        public static G2M_RequestExitGame Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<G2M_RequestExitGame>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.M2G_RequestExitGame)]
    public partial class M2G_RequestExitGame : MessageObject, ILocationResponse
    {
        public static M2G_RequestExitGame Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<M2G_RequestExitGame>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.G2M_SecondLogin)]
    [ResponseType(nameof(M2G_SecondLogin))]
    public partial class G2M_SecondLogin : MessageObject, ILocationRequest
    {
        public static G2M_SecondLogin Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<G2M_SecondLogin>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;

            ObjectPool.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(LoginInner.M2G_SecondLogin)]
    public partial class M2G_SecondLogin : MessageObject, ILocationResponse
    {
        public static M2G_SecondLogin Create(bool isFromPool = false)
        {
            return ObjectPool.Fetch<M2G_SecondLogin>(isFromPool);
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;

            ObjectPool.Recycle(this);
        }
    }

    public static class LoginInner
    {
        public const ushort R2G_GetLoginKey = 20002;
        public const ushort G2R_GetLoginKey = 20003;
        public const ushort G2M_SessionDisconnect = 20004;
        public const ushort R2L_LoginAccountRequest = 20005;
        public const ushort L2R_LoginAccountRequest = 20006;
        public const ushort L2G_DisconnectGateUnit = 20007;
        public const ushort G2L_DisconnectGateUnit = 20008;
        public const ushort G2L_AddLoginRecord = 20009;
        public const ushort L2G_AddLoginRecord = 20010;
        public const ushort G2L_RemoveLoginRecord = 20011;
        public const ushort L2G_RemoveloginRecord = 20012;
        public const ushort G2M_RequestExitGame = 20013;
        public const ushort M2G_RequestExitGame = 20014;
        public const ushort G2M_SecondLogin = 20015;
        public const ushort M2G_SecondLogin = 20016;
    }
}