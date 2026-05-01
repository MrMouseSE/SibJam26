using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts
{
    public class RecipeHandlerMechanic : IGameMechanic
    {
        public RecipeHandlerComponent Component;

        public RecipeHandlerMechanic(RecipeHandlerComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeMechanic()
        {
        }
    }
}