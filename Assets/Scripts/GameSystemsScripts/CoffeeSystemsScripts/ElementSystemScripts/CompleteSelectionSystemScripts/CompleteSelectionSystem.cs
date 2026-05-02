using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class CompleteSelectionSystem : IGameSystem
    {
        public CompleteSelectionComponent Component;
        public CompleteSelectionMechanic Mechanic;

        public CompleteSelectionSystem(ElementDrawStateButtonContainer container, GameSystemsHandler gameSystemsHandler)
        {
            Component = new CompleteSelectionComponent(container);
            Mechanic = new CompleteSelectionMechanic(Component, gameSystemsHandler);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
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