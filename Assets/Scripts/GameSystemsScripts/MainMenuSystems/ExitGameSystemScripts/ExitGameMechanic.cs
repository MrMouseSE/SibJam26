using Cysharp.Threading.Tasks;
using ScenesOperatingScripts;

namespace GameSystemsScripts.MainMenuSystems.ExitGameSystemScripts
{
    public class ExitGameMechanic : IGameMechanic
    {
        public ExitGameComponent Component;
        public ExitGameMechanic(ExitGameComponent component)
        {
            Component = component;
            Component.Container.OnButtonPushed += ExitGameMethod;
        }

        private void ExitGameMethod()
        {
            Component.IsGameExitProcess = true;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeMechanic()
        {
            Component.Container.OnButtonPushed -= ExitGameMethod;
            ExitApplication().Forget();
        }

        private async UniTaskVoid ExitApplication()
        {
            await UniTask.WaitForSeconds(1f);
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}