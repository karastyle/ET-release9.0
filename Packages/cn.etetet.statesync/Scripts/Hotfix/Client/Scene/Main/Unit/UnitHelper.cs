namespace ET.Client
{
    public static partial class UnitHelper
    {
        public static Unit GetMyUnitFromClientScene(Scene root)
        {
            AccountModel accountModel = root.GetComponent<AccountModel>();
            Scene currentScene = root.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(accountModel.RoleId);
        }
        
        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            AccountModel accountModel = currentScene.Root().GetComponent<AccountModel>();
            return currentScene.GetComponent<UnitComponent>().Get(accountModel.RoleId);
        }
    }
}