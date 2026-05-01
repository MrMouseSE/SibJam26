using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts
{
    public class SelectionButtonsSystem : IGameSystem
    {
        public SelectionButtonComponent Component;
        public SelectionButtonMechanic Mechanic;

        public SelectionButtonsSystem(ElementDrawStateButtonContainer completeButton, ElementDrawStateButtonContainer redrawButton)
        {
            Component = new SelectionButtonComponent(completeButton, redrawButton);
            Mechanic = new SelectionButtonMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}