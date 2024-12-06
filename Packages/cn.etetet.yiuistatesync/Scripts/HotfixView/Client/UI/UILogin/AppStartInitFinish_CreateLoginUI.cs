
namespace ET.Client
{
	[Event(SceneType.StateSync)]
	public class AppStartInitFinish_CreateLoginUI: AEvent<Scene, AppStartInitFinish>
	{
		protected override async ETTask Run(Scene root, AppStartInitFinish args)
		{
			//直接进入LoginMode
			ModeMgr modeMgr = root.GetComponent<ModeMgr>();
			modeMgr.EnterGameMode(CPlayMode.Login);
			await ETTask.CompletedTask;
		}
	}
}
