using CameraScripts;

namespace GameSystemsScripts.CameraSystem
{
    public class GameCameraComponent
    {
        public bool IsCameraUpdating;
        public CameraContainer CurrentCamera;
        public CameraHolder CurrentCameraHolder;

        public GameCameraComponent(CameraContainer container)
        {
            CurrentCamera = container;
        }
    }
}