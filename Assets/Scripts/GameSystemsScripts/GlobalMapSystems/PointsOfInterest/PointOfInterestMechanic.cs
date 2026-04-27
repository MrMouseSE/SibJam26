using GameSystemsScripts.CameraSystem;
using GlobalMapScripts.GlobalMapPointsOfInterest;
using ScenesOperatingScripts;
using SupportScripts;

namespace GameSystemsScripts.GlobalMapSystems.PointsOfInterest
{
    public class PointOfInterestMechanic : IGameMechanic
    {
        public PointOfInterestComponent Component;

        public PointOfInterestMechanic(PointOfInterestComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var inputComponent = ((GameInputSystem.GameInputSystem)gameSystemsHandler.GetGameSystem(typeof(GameInputSystem.GameInputSystem))).Component;
            if (!inputComponent.MouseWasPressedThisFrame) return;
            StaticSupportMethods.GetAllCollidersFromMouseCast(
                ((GameCameraSystem)gameSystemsHandler.GetGameSystem(typeof(GameCameraSystem))).Component.CurrentCamera.CameraObject,
                inputComponent.MousePressedPosition);
        }

        public void DisposeMechanic()
        {
        }
    }
}