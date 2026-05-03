using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.ShowCoffeScripts
{
    public class ShowCoffeeSystem : IGameSystem
    {
        public ShowCoffeeComponent Component;
        public ShowCoffeeMechanic Mechanic;

        public ShowCoffeeSystem()
        {
            Component = new ShowCoffeeComponent();
            Mechanic = new ShowCoffeeMechanic(Component);
        }
        
        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.CoffeeShow) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}