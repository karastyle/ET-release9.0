namespace ET.Client
{
    public static partial class UnitHelper
    {
        public static Unit GetMyUnitFromClientScene(Scene root)
        {
            AccountComponent accountComponent = root.GetComponent<AccountComponent>();
            Scene currentScene = root.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(accountComponent.RoleId);
        }
        
        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            AccountComponent accountComponent = currentScene.Root().GetComponent<AccountComponent>();
            return currentScene.GetComponent<UnitComponent>().Get(accountComponent.RoleId);
        }
    }
}