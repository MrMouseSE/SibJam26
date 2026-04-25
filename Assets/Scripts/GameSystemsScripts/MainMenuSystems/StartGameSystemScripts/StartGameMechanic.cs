using GameStartupScripts;
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

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var root = SceneLoadingHandler.SetSceneActive(SceneNamesConst.GameScene);
            var cameraSystem = gameSystemsHandler.GetGameSystem((typeof(CameraSystem.CameraSystem))) as CameraSystem.CameraSystem;
            cameraSystem.Component.CurrentCameraHolder = root.GetCameraHolder();
            cameraSystem.Component.IsCameraUpdating = true;
        }

        public void DisposeMechanic()
        {
            Component.ButtonContainer.OnButtonPushed -= StartGameButtonPushed;
        }
    }
}