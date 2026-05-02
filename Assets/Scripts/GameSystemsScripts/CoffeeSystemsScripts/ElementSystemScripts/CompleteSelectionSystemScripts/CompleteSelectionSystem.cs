using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionSystem : IGameSystem
    {
        public CompleteSelectionComponent Component;
        public CompleteSelectionMechanic Mechanic;

        public CompleteSelectionSystem()
        {
            Component = new CompleteSelectionComponent();
            Mechanic = new CompleteSelectionMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            Mechanic.SetSystemHandler(gameSystemsHandler);
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