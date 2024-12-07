namespace ET
{
    [EntitySystemOf(typeof(RoleUnit))]
    public static partial class RoleUnitSystem
    {
        public static void FromMessage(this RoleUnit self, RoleInfoProto roleInfoProto)
        {
            self.Name = roleInfoProto.Name;
            self.State = roleInfoProto.State;
            self.Account = roleInfoProto.Account;
            self.CreateTime = roleInfoProto.CreateTime;
            self.ServerId = roleInfoProto.ServerId;
            self.LastLoginTime = roleInfoProto.LastLoginTime;
            self.HeroId = roleInfoProto.HeroId;
        }

        public static RoleInfoProto ToMessage(this RoleUnit self)
        {
            RoleInfoProto roleInfoProto = RoleInfoProto.Create();
            roleInfoProto.Id = self.Id;
            roleInfoProto.Name = self.Name;
            roleInfoProto.State = self.State;
            roleInfoProto.Account = self.Account;
            roleInfoProto.CreateTime = self.CreateTime;
            roleInfoProto.ServerId = self.ServerId;
            roleInfoProto.LastLoginTime = self.LastLoginTime;
            roleInfoProto.HeroId = self.HeroId;
            return roleInfoProto;
        }

        public static string ToString(this RoleUnit self)
        {
            return $"  uid:{self.Id}\t NickName:{self.Name}\t level:{self.level}\t exp:{self.exp}";
        }
        
        [EntitySystem]
        private static void Awake(this ET.RoleUnit self)
        {

        }
    }
}
