namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class PlayerComponent: Entity, IAwake
    {
        //账号登录后拿到的token
        public string Token { get; set; }
        
        public string Account { get; set; }
    }
}