using CoffeeScripts.ElementsInventoryScripts;
using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.AproveRecipeScripts
{
    public class ApproveRecipeMechanic : IGameMechanic
    {
        public ApproveRecipeComponent Component;

        public ApproveRecipeMechanic(ApproveRecipeComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            
            foreach (var recipeContainer in fillRecipeSystem.Component.RecipeContainers)
            {
                ElementsStaticInventory.RemoveElementInInventory(recipeContainer.ElementName);
            }
            
            fillRecipeSystem.Component.RecipeContainers.Clear();
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CoffeeShow);
        }

        public void DisposeMechanic()
        {
        }
    }
}