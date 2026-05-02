using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.StartBoilingScripts
{
    public class StartBoilingMechanic : IGameMechanic
    {
        public StartBoilingComponent Component;

        public StartBoilingMechanic(StartBoilingComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            CompleteBoilingSystem completeBoilingSystem = (CompleteBoilingSystem)gameSystemsHandler.GetGameSystem(typeof(CompleteBoilingSystem));
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            float duration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
            completeBoilingSystem.Component.CompleteBoilingButtonContainer.SetActive(true, duration);
            BoilingProcessSystem processSystem = (BoilingProcessSystem)gameSystemsHandler.GetGameSystem(typeof(BoilingProcessSystem));
            var boilCoffeeContainer = processSystem.Component.Container;
            boilCoffeeContainer.IndicatorShakeTweenGroup.AnimateByUpdate = true;
            boilCoffeeContainer.ProcessBoilTweenGroup.AnimateByUpdate = true;
            
            GameCameraSystem cameraSystem = (GameCameraSystem)gameSystemsHandler.GetGameSystem(typeof(GameCameraSystem));
            cameraSystem.Mechanic.AnimateCameraMovement(boilCoffeeContainer.NormalCameraPoint, boilCoffeeContainer.BoildFocusCameraPoint,
                speedSystem.Component.AnimationsDescription.Camera);
        }

        public void DisposeMechanic()
        {
        }
    }
}