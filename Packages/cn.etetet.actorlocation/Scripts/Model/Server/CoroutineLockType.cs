namespace ET
{
    public static partial class CoroutineLockType
    {
        public const int Location = PackageType.ActorLocation * 1000 + 1;                  // location进程上使用
        public const int MessageLocationSender = PackageType.ActorLocation * 1000 + 2;       // MessageLocationSender中队列消息 
        public const int LoginAccount = PackageType.ActorLocation * 1000 + 3;
        public const int CreateRole = PackageType.ActorLocation * 1000 + 4;
        public const int LoginCenterLock = PackageType.ActorLocation * 1000 + 5;
        public const int LoginGate = PackageType.ActorLocation * 1000 + 6;
        
    }
}