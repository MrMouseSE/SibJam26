using GameScripts;
using GameSystemsScripts.CameraSystem;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;

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
            Component.CompleteBoilingButtonContainer = buttonContainer;
            Component.CompleteBoilingButtonContainer.OnButtonPressed += OnButtonClicked;
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