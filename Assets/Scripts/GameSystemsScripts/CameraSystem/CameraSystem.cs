using CameraScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CameraSystem
{
    public class CameraSystem : IGameSystem
    {
        public CameraMechanic Mechanic;
        public CameraComponent Component;

        public CameraSystem(CameraContainer container)
        {
            Component = new CameraComponent(container);
            Mechanic = new CameraMechanic(Component);
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