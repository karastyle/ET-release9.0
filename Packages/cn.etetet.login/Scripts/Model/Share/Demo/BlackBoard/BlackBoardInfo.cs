namespace ET
{
    //黑板数据
    public enum BlackBoardEnum
    {
        IncrementRoleId = 0  //角色自增id
    }
    
    [ChildOf]
    public class BlackBoardInfo: Entity, IAwake
    {
        public int Key { get; set; }
        public long Value { get; set; }
    }
}

