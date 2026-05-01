using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionSystem : IGameSystem
    {
        public CompleteSelectionComponent Component;
        public CompleteSelectionMechanic Mechanic;

        public CompleteSelectionSystem(CompleteSelectionButtonContainer container)
        {
            Component = new CompleteSelectionComponent(container);
            Mechanic = new CompleteSelectionMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
            throw new System.NotImplementedException();
        }
    }
}