using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.SelectionButtonsScripts
{
    public class SelectionButtonsSystem : IGameSystem
    {
        public SelectionButtonComponent Component;
        public SelectionButtonMechanic Mechanic;

        public SelectionButtonsSystem()
        {
            Component = new SelectionButtonComponent();
            Mechanic = new SelectionButtonMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            Mechanic.InitializeButtonsValues(gameSystemsHandler);
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