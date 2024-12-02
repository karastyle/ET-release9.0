namespace ET.Server
{
    [MessageHandler(SceneType.Map)]
    public class G2M_RequestExitGameHandler : MessageLocationHandler<Unit, G2M_RequestExitGame, M2G_RequestExitGame>
    {
        protected override async ETTask Run(Unit unit, G2M_RequestExitGame request, M2G_RequestExitGame response)
        {
            await ETTask.CompletedTask;
            
            // Unit角色下线业务， 保存数据到数据库
            
            //释放Unit
            RemoveUnit(unit).NoContext();
        }

        private async ETTask RemoveUnit(Unit unit)
        {
            //要等消息返回
            await unit.Fiber().WaitFrameFinish();
            
            await unit.RemoveLocation(LocationType.Unit);
            UnitComponent unitComponent = unit.Root().GetComponent<UnitComponent>();
            unitComponent.Remove(unit.Id);
        }
    }
}