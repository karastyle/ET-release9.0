namespace ET
{
    [EntitySystemOf(typeof(RoleInfo))]
    public static partial class RoleInfoSystem
    {
        public static void FromMessage(this RoleInfo self, RoleInfoProto roleInfoProto)
        {
            self.uid = roleInfoProto.Id;
            self.Name = roleInfoProto.Name;
            self.State = roleInfoProto.State;
            self.Account = roleInfoProto.Account;
            self.CreateTime = roleInfoProto.CreateTime;
            self.ServerId = roleInfoProto.ServerId;
            self.LastLoginTime = roleInfoProto.LastLoginTime;
            self.HeroId = roleInfoProto.HeroId;
        }

        public static RoleInfoProto ToMessage(this RoleInfo self)
        {
            RoleInfoProto roleInfoProto = RoleInfoProto.Create();
            roleInfoProto.Id = self.uid;
            roleInfoProto.Name = self.Name;
            roleInfoProto.State = self.State;
            roleInfoProto.Account = self.Account;
            roleInfoProto.CreateTime = self.CreateTime;
            roleInfoProto.ServerId = self.ServerId;
            roleInfoProto.LastLoginTime = self.LastLoginTime;
            roleInfoProto.HeroId = self.HeroId;
            return roleInfoProto;
        }
        
        public static string ToString(this RoleInfo self)
        {
            return $"  uid:{self.uid}\t NickName:{self.Name}\t level:{self.level}\t exp:{self.exp}";
        }
    }
}
