using System.Collections.Generic;
using System.Linq;
using CoffeeScripts;
using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.RecipeHandlerScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.CompareRecipeScripts
{
    public class CompareRecipeMechanic : IGameMechanic
    {
        public CompareRecipeComponent Component;

        public CompareRecipeMechanic(CompareRecipeComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var recipeHandlerSystem = (RecipeHandlerSystem)gameSystemsHandler.GetGameSystem(typeof(RecipeHandlerSystem));
            var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            List<CoffeeRecipeDescription> matchedRecipes = 
                recipeHandlerSystem.Component.CurrentCoffeeDescription.Recipes.Where(recipe => recipe.CompareWithRecipe(fillRecipeSystem.Component.RecipeContainers)).ToList();
            Component.CurrentRecipe = matchedRecipes.Count > 0 ? 
                matchedRecipes.OrderByDescending(x=>x.RecipeMultiplier).ToList()[0] : recipeHandlerSystem.Component.CurrentCoffeeDescription.DefaultRecipe;
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.StartBoil);
        }

        public void DisposeMechanic()
        {
        }
    }
}