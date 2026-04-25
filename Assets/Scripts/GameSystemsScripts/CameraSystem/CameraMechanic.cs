using ScenesOperatingScripts;

namespace GameSystemsScripts.CameraSystem
{
    public class CameraMechanic : IGameMechanic
    {
        public CameraComponent Component;

        public CameraMechanic(CameraComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Component.CurrentCamera.CameraTransform.position = Component.CurrentCameraHolder.CameraRoot.position;
            Component.CurrentCamera.CameraTransform.rotation = Component.CurrentCameraHolder.CameraRoot.rotation;
        }
        
        public void DisposeMechanic()
        {
        }
    }
}