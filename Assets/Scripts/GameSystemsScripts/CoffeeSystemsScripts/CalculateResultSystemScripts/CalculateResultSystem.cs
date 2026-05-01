using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.CalculateResultSystemScripts
{
    public class CalculateResultSystem : IGameSystem
    {
        public CalculateResultComponent Component;
        public CalculateResultMechanic Mechanic;

        public CalculateResultSystem()
        {
            Component = new CalculateResultComponent();
            Mechanic = new CalculateResultMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.CalculateValue) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}