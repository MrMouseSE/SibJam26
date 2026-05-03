using GameScripts;
using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts
{
    public class CompleteBoilingMechanic : IGameMechanic
    {
        public CompleteBoilingComponent Component;

        public CompleteBoilingMechanic(CompleteBoilingComponent component)
        {
            Component = component;
        }
        
        public void SetButtonContainer(BoilButtonContainer buttonContainer)
        {
            buttonContainer.ButtonTooltipContainer.AppearAnimation.SetForceState(true);
            Component.CompleteBoilingButtonContainer = buttonContainer;
            Component.CompleteBoilingButtonContainer.OnButtonPressed += OnButtonClicked;
            Component.CompleteBoilingButtonContainer.ButtonActivateAnimations.SetForceState(true);
        }

        public void PlayTooltip(GameSpeedSystem speedSystems)
        {
            var cont = Component.CompleteBoilingButtonContainer.ButtonTooltipContainer;
            float duration = speedSystems.Component.AnimationsDescription.ButtonTooltipAnimationDuration;
            UniTaskAnimationLazyObject animObj = new(cont.AppearAnimation, ref cont.CancelToken, duration, true);
            animObj.Play().Forget();
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsBoilingComplete) return;
            Component.IsBoilingComplete = false;
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            float duration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
            Component.CompleteBoilingButtonContainer.SetActive(false, duration);
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CalculateValue);
        }

        private void OnButtonClicked()
        {
            Component.IsBoilingComplete = true;
        }

        public void DisposeMechanic()
        {
            Component.CompleteBoilingButtonContainer.OnButtonPressed -= OnButtonClicked;
        }
    }
}