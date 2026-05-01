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

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsSelectionStateStartedThisFrame) return;
            Component.IsSelectionStateStartedThisFrame = false;
            Component.CompleteButton.SetActive(true);
            Component.RedrawButton.SetActive(true);
        }

        public void DisposeMechanic()
        {
        }
    }
}