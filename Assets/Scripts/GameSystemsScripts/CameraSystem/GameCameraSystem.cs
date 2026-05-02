using CameraScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CameraSystem
{
    public class GameCameraSystem : IGameSystem
    {
        public GameCameraMechanic Mechanic;
        public GameCameraComponent Component;

        public GameCameraSystem(CameraContainer container)
        {
            Component = new GameCameraComponent(container);
            Mechanic = new GameCameraMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsCameraUpdating) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
            Mechanic.DisposeMechanic();
        }
    }
}