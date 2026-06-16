using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.BoilingProcessScripts;
using GameSystemsScripts.CoffeeSystemsScripts.BoilingSystemScripts.CompleteBoilingScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.CompareRecipeScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using GameSystemsScripts.GameSpeedScripts;
using GameSystemsScripts.ScoreViewScripts.InterfaceScoreScripts;
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
            var boostersSystem = (BoostersSystem)gameSystemsHandler.GetGameSystem(typeof(BoostersSystem));
            var scoreSystem = (InterfaceScoreSystem)gameSystemsHandler.GetGameSystem(typeof(InterfaceScoreSystem));
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            
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

            Component.RecipeCostSumm = compareSystem.Component.CurrentRecipe.RecipeCost;
            Component.RecipeMultiplier = compareSystem.Component.CurrentRecipe.RecipeMultiplier;
            Component.BoilMultiplier = boilSystem.Component.BoilMultiplier;
            Component.BoostersMultiplier = boostersSystem.Component.CurrentMultiplier;

            Component.ResultValue = CalculateCoffeeValue();
            scoreSystem.Mechanic.UpdateScore(Component.ResultValue, speedSystem.Component.AnimationsDescription.ScoreAnimationDuration);
            
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.ApproveRecipe);
        }

        private float CalculateCoffeeValue()
        {
            return (Component.VigorSumm * Component.VigorMultiplierSumm + Component.TasteSumm * Component.TasteMultiplierSumm + Component.RecipeCostSumm)
                   * Component.RecipeMultiplier * Component.BoilMultiplier * Component.BoostersMultiplier;
        }

        public void DisposeMechanic()
        {
        }
    }
}
