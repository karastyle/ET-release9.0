namespace ET
{
    public enum RoleInfoState
    {
        Normal = 0,
        Freeze,
    }

    [ChildOf]
    public class RoleInfo : Entity, IAwake
    {
        public string Name;
        public int ServerId;
        public int State;
        public string Account;
        public long LastLoginTime;
        public long CreateTime;

        public long uid { get; set; }

        public float posX { get; set; }
        public float posZ { get; set; }
        public float dirX { get; set; }
        public float dirZ { get; set; }

        public ulong lastRid { get; set; }
        public ulong lastTid { get; set; }

        public int unitID { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
    }
}