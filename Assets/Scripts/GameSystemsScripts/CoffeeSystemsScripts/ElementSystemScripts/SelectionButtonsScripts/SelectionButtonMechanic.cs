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

        public void InitializeButtonsValues(GameSystemsHandler gameSystemsHandler)
        {
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            Component.CompleteButton.HoverAnimationDuration = speedSystem.Component.AnimationsDescription.ButtonHoverAnimationDuration;
            Component.CompleteButton.ClickAnimationDuration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
            Component.RedrawButton.HoverAnimationDuration = speedSystem.Component.AnimationsDescription.ButtonHoverAnimationDuration;
            Component.RedrawButton.ClickAnimationDuration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsSelectionStateStartedThisFrame) return;
            Component.IsSelectionStateStartedThisFrame = false;
            GameSpeedSystem gameSpeed = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            Component.CompleteButton.SetActive(true, gameSpeed.Component.AnimationsDescription.ButtonAnimationDuration);
            Component.RedrawButton.SetActive(true, gameSpeed.Component.AnimationsDescription.ButtonAnimationDuration);
        }

        public void DisposeMechanic()
        {
        }
    }
}