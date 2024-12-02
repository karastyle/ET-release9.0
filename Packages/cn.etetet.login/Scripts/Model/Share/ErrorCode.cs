namespace ET
{
    public static partial class ErrorCode
    {
        private const int prefix = ERR_WithException + PackageType.Login * 1000;
        public const int ERR_ConnectGateKeyError = prefix + 1;
        
        public const int ERR_LoginInfoEmpty = prefix + 2;
        public const int ERR_LoginPasswordError = prefix + 3;

        public const int ERR_RequestRepeatedly = prefix + 4;
        public const int ERR_LoginInfoIsNull = prefix + 5;
        public const int ERR_AccountNameFormError = prefix + 6;
        public const int ERR_PasswordFormError = prefix + 7;
        public const int ERR_AccountInBlackListError = prefix + 8;
        
        public const int ERR_TokenError = prefix + 9;
        public const int ERR_RoleNameIsNull = prefix + 10;
        public const int ERR_RoleNameSame = prefix + 11;
        public const int ERR_RoleNotExist = prefix + 12;
        
        public const int ERR_LoginGameGateError01 = prefix + 13;
        public const int ERR_OtherAccountLogin = prefix + 14;
        
        public const int ERR_SessionPlayerError = prefix + 15;
        
        public const int ERR_NonePlayerError = prefix + 16;
        
        public const int ERR_PlayerSessionError = prefix + 17;
        public const int ERR_EnterGameError = prefix + 18;
        
        public const int ERR_ReEnterGameError = prefix + 19;
        public const int ERR_ReEnterGameError2 = prefix + 20;
    }
}