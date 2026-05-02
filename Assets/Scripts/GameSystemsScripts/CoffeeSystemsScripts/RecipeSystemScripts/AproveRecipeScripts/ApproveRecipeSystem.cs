using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.AproveRecipeScripts
{
    public class ApproveRecipeSystem : IGameSystem
    {
        public ApproveRecipeComponent Component;
        public ApproveRecipeMechanic Mechanic;

        public ApproveRecipeSystem()
        {
            Component = new ApproveRecipeComponent();
            Mechanic = new ApproveRecipeMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.ApproveRecipe) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}