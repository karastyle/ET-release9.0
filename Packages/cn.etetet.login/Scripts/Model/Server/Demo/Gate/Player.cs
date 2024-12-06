namespace ET.Server
{
    public enum PlayerState
    {
        Disconnect,
        Gate,
        Game,
    }
    
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<string>
    {
        public string Account { get; set; }
        
        public PlayerState PlayerState { get; set; }

        //当前登录的角色
        public long RoleId { get; set; }
    }
}