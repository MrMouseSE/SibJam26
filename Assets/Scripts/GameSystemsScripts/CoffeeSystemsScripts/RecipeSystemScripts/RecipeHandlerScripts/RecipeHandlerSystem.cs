using CoffeeScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts
{
    public class RecipeHandlerSystem : IGameSystem
    {
        public RecipeHandlerComponent Component;
        public RecipeHandlerMechanic Mechanic;

        public RecipeHandlerSystem(CoffeeDescription description)
        {
            Component = new RecipeHandlerComponent(description);
            Mechanic = new RecipeHandlerMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeSystem()
        {
        }
    }
}