using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.CompareRecipeScripts
{
    public class CompareRecipeSystem : IGameSystem
    {
        public CompareRecipeComponent Component;
        public CompareRecipeMechanic Mechanic;

        public CompareRecipeSystem()
        {
            Component = new CompareRecipeComponent();
            Mechanic = new CompareRecipeMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.CompareRecipe) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}