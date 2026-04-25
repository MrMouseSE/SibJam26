using GameStartupScripts;
using MainMenuScripts.StartGameSystemScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.MainMenuSystems.StartGameSystemScripts
{
    public class StartGameMechanic : IGameMechanic
    {
        public StartGameComponent Component;
        public StartGameMechanic(StartGameComponent component)
        {
            Component = component;
            Component.ButtonContainer.OnButtonPushed += StartGameButtonPushed;
        }

        private void StartGameButtonPushed()
        {
            Component.IsStartGameButtonPushed = true;
        }

        public void StartGame()
        {
            SceneLoadingHandler.SetSceneActive(SceneNamesConst.GameScene);
        }

        public void DisposeMechanic()
        {
            Component.ButtonContainer.OnButtonPushed -= StartGameButtonPushed;
        }
    }
}