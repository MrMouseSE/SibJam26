using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.CompareRecipeScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.CalculateResultSystemScripts
{
    public class CalculateResultMechanic : IGameMechanic
    {
        public CalculateResultComponent Component;

        public CalculateResultMechanic(CalculateResultComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            float vigorSumm = 0f;
            float vigorMult = 0f;
            float tasteSumm = 0f;
            float tasteMult = 0f;
            var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            foreach (var container in fillRecipeSystem.Component.RecipeContainers)
            {
                vigorSumm += container.ElementVigorValue;
                vigorMult += container.ElementVigorMultiplier;
                tasteSumm += container.ElementTesteValue;
                tasteMult += container.ElementTesteMultiplier;
            }
            Component.VigorSumm = vigorSumm;
            Component.VigorMultiplierSumm = vigorMult;
            Component.TasteSumm = tasteSumm;
            Component.TasteMultiplierSumm = tasteMult;
            Component.RecipeMultiplier = 
                ((CompareRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(CompareRecipeSystem))).Component.CurrentRecipe.RecipeMultiplier;
            Component.ResultValue = (vigorSumm * vigorMult + tasteSumm * tasteMult) * Component.RecipeMultiplier;
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.ApproveRecipe);
        }

        public void DisposeMechanic()
        {
        }
    }
}