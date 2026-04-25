using CameraScripts;

namespace GameSystemsScripts.CameraSystem
{
    public class CameraComponent
    {
        public bool IsCameraUpdating;
        public CameraContainer CurrentCamera;
        public CameraHolder CurrentCameraHolder;

        public CameraComponent(CameraContainer container)
        {
            CurrentCamera = container;
        }
    }
}