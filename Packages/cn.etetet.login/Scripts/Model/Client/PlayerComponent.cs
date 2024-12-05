﻿namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class PlayerComponent: Entity, IAwake
    {
        //账号登录后拿到的token
        public string Token { get; set; }
        
        //账号名字
        public string Account { get; set; }
        
        //当前登录的服
        public int ServerId { get; set; }
    }
}