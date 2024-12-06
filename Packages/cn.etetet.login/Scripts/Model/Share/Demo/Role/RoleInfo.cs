namespace ET
{
    public enum RoleInfoState
    {
        Normal = 0,
        Freeze,
    }

    public enum RoleShowType
    {
        Role = 0,
        Empty = 1,
        Lock = 3,
    }

    [ChildOf]
    public class RoleInfo : Entity, IAwake
    {
        public string Name { get; set; }
        public int ServerId { get; set; }
        public int State { get; set; }
        public string Account { get; set; }
        public long LastLoginTime { get; set; }
        public long CreateTime { get; set; }
        public int HeroId { get; set; }

        public float posX { get; set; }
        public float posZ { get; set; }
        public float dirX { get; set; }
        public float dirZ { get; set; }

        public ulong lastRid { get; set; }
        public ulong lastTid { get; set; }

        public int level { get; set; }
        public int exp { get; set; }

        public int ShowType { get; set; } = (int)RoleShowType.Role;
    }
    public class BlackBoardInfo2: Entity
    {
        public long RoleIncrementId { get; set; }
    }
}