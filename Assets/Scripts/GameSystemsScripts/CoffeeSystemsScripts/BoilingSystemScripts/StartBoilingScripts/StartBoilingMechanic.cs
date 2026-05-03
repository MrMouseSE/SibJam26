using GameScripts;
using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;
using TweenScripts;

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
            var completeBoilingSystem = (CompleteBoilingSystem)gameSystemsHandler.GetGameSystem(typeof(CompleteBoilingSystem));
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            var processSystem = (BoilingProcessSystem)gameSystemsHandler.GetGameSystem(typeof(BoilingProcessSystem));
            GameCameraSystem cameraSystem = (GameCameraSystem)gameSystemsHandler.GetGameSystem(typeof(GameCameraSystem));
            
            float duration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
            completeBoilingSystem.Component.CompleteBoilingButtonContainer.SetActive(true, duration);
            completeBoilingSystem.Mechanic.PlayTooltip(speedSystem);
            
            var boilCoffeeContainer = processSystem.Component.Container;
            boilCoffeeContainer.SoundContainer.Play(SoundType.AppearSound);
            boilCoffeeContainer.IndicatorShakeTweenGroup.AnimateByUpdate = true;
            boilCoffeeContainer.ProcessBoilTweenGroup.AnimateByUpdate = true;
            
            cameraSystem.Mechanic.AnimateCameraMovement(boilCoffeeContainer.NormalCameraPoint, boilCoffeeContainer.BoildFocusCameraPoint,
                speedSystem.Component.AnimationsDescription.Camera);
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.BoilingCoffee);
        }

        public void DisposeMechanic()
        {
        }
    }
}