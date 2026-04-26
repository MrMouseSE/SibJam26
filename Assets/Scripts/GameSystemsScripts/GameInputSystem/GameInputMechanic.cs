using ScenesOperatingScripts;
using SupportScripts;
using UnityEngine.InputSystem;

namespace GameSystemsScripts.GameInputSystem
{
    public class GameInputMechanic : IGameMechanic
    {
        public GameInputComponent Component;

        public GameInputMechanic(GameInputComponent component)
        {
            Component = component;
        }
        
        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Component.MouseWasPressedThisFrame = Mouse.current.leftButton.wasPressedThisFrame;
            if (Component.MouseWasPressedThisFrame)
            {
                Component.MousePressedPosition = Mouse.current.position.ReadValue();
                var camera = gameSystemsHandler.GetCameraSystem().Component.CurrentCamera.CameraObject;
                Component.MousePressedHitColliders = StaticSupportMethods.GetAllCollidersFromMouseCast(camera, 
                        Component.MousePressedPosition);
            }
            
            Component.MouseWasReleasedThisFrame = Mouse.current.rightButton.wasPressedThisFrame;
            if (Component.MouseWasReleasedThisFrame)
            {
                Component.MouseReleasedPosition = Mouse.current.position.ReadValue();
                var camera = gameSystemsHandler.GetCameraSystem().Component.CurrentCamera.CameraObject;
                Component.MousePressedHitColliders = StaticSupportMethods.GetAllCollidersFromMouseCast(camera, 
                        Component.MousePressedPosition);
            }
        }
        
        public void DisposeMechanic()
        {
        }
    }
}