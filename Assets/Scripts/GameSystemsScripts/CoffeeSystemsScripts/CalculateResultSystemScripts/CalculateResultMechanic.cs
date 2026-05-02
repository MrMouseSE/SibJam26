using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
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
            var fillRecipeSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            var compareSystem = (CompareRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(CompareRecipeSystem));
            var boilSystem = (BoilingProcessSystem)gameSystemsHandler.GetGameSystem(typeof(BoilingProcessSystem));
            
            float vigorSumm = 0f;
            float vigorMult = 0f;
            float tasteSumm = 0f;
            float tasteMult = 0f;
            
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
            
            Component.RecipeMultiplier = compareSystem.Component.CurrentRecipe.RecipeMultiplier;
            Component.BoilMultiplier = boilSystem.Component.BoilMultiplier;

            Component.ResultValue = CalculateCoffeeValue();
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.ApproveRecipe);
        }

        private float CalculateCoffeeValue()
        {
            return (Component.VigorSumm * Component.VigorMultiplierSumm + Component.TasteSumm * Component.TasteMultiplierSumm)
                   * Component.RecipeMultiplier * Component.BoilMultiplier;
        }

        public void DisposeMechanic()
        {
        }
    }
}