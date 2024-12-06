using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(KeyBoardComponent))]
    public static partial class KeyBoardComponentSystem
    {
        [EntitySystem]
        private static void Update(this KeyBoardComponent self)
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Log.Debug("Reload Hotfix code");
                CodeLoader.Instance.Reload();
            }
        }
        [EntitySystem]
        private static void Awake(this ET.Client.KeyBoardComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.KeyBoardComponent self)
        {

        }
    }
}

