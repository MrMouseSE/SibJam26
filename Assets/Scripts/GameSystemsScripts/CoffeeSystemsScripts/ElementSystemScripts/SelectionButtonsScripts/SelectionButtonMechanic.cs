using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts
{
    public class SelectionButtonMechanic : IGameMechanic
    {
        public SelectionButtonComponent Component;

        public SelectionButtonMechanic(SelectionButtonComponent component)
        {
            Component = component;
        }

        public void SetButtonsContainers(GameButtonContainer complete, GameButtonContainer redraw, GameSystemsHandler gameSystemsHandler)
        {
            complete.ButtonTooltipContainer.AppearAnimation.SetForceState(true);
            redraw.ButtonTooltipContainer.AppearAnimation.SetForceState(true);
            Component.CompleteButton = complete;
            Component.RedrawButton = redraw;
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            Component.CompleteButton.HoverAnimationDuration = speedSystem.Component.AnimationsDescription.HoverAnimationDuration;
            Component.CompleteButton.ClickAnimationDuration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
            Component.RedrawButton.HoverAnimationDuration = speedSystem.Component.AnimationsDescription.HoverAnimationDuration;
            Component.RedrawButton.ClickAnimationDuration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
        }
        
        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsSelectionStateStartedThisFrame) return;
            Component.IsSelectionStateStartedThisFrame = false;
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            Component.CompleteButton.SetActive(true, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);
            Component.RedrawButton.SetActive(true, speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);
        }

        public void DisposeMechanic()
        {
        }
    }
}