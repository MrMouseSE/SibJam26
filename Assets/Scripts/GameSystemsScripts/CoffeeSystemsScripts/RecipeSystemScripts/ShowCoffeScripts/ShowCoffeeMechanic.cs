using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.CalculateResultSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.CompareRecipeScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using SoundsComponentsScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.ShowCoffeScripts
{
    public class ShowCoffeeMechanic : IGameMechanic
    {
        public ShowCoffeeComponent Component;
        
        private GameSystemsHandler _gameSystemsHandler;

        public ShowCoffeeMechanic(ShowCoffeeComponent component)
        {
            Component = component;
        }

        public void SetContainer(ShowCoffeeContainer container, GameSystemsHandler gameSystemsHandler)
        {
            container.CoffeShowAnimation.SetForceState(true);
            Component.Container = container;
            _gameSystemsHandler = gameSystemsHandler;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (Component.IsCoffeeShowing) return;
            Component.IsCoffeeShowing = true;
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            var recipeSystem = (CompareRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(CompareRecipeSystem));
            var resultSystem = (CalculateResultSystem)gameSystemsHandler.GetGameSystem(typeof(CalculateResultSystem));
            var result = resultSystem.Component;
            var container = Component.Container;
            var recipe = recipeSystem.Component.CurrentRecipe;
            container.CoffeShowRenderer.sprite = recipe.CoffeeSprite;
            container.CoffeNameText.text = recipe.RecipeName;
            
            SetCounts(container, result);
            
            /*container.ElementsVigodText.text = result.VigorSumm.ToString("0");
            container.ElementsVigodMultText.text = result.VigorMultiplierSumm.ToString("0");
            container.ElementsTasteText.text = result.TasteSumm.ToString("0");
            container.ElementTasteMultText.text = result.TasteMultiplierSumm.ToString("0");
            container.CoffeValueText.text = result.RecipeCostSumm.ToString("0");
            container.CoffeMultText.text = result.RecipeMultiplier.ToString("0");
            container.BoilValueText.text = result.BoilMultiplier.ToString("0");
            container.FullScoreText.text = result.ResultValue.ToString("0");*/
            
            container.CoffeShowAudioContainer.Play(SoundType.AppearSound);
            UniTaskAnimationLazyObject animObj = new(container.CoffeShowAnimation, ref container.CancelToken,
                speedSystem.Component.AnimationsDescription.ShowCoffeeDuration,true);
            animObj.AnimationCompleted += OnShowEnd;
            animObj.Play().Forget();
        }

        private void SetCounts(ShowCoffeeContainer container, CalculateResultComponent result)
        {
            container.Vigor.ToCount = result.VigorSumm;
            container.Vigor.AnimateByUpdate = true;
            container.VigorMult.Loop = false;
            container.VigorMult.ToCount = result.VigorMultiplierSumm;
            container.VigorMult.AnimateByUpdate = true;
            container.VigorMult.Loop = false;
            container.Taste.ToCount = result.TasteSumm;
            container.Taste.AnimateByUpdate = true;
            container.TasteMult.Loop = false;
            container.TasteMult.ToCount = result.TasteMultiplierSumm;
            container.TasteMult.AnimateByUpdate = true;
            container.TasteMult.Loop = false;
            container.Coffe.ToCount = result.RecipeCostSumm;
            container.Coffe.AnimateByUpdate = true;
            container.Coffe.Loop = false;
            container.CoffeMult.ToCount = result.RecipeMultiplier;
            container.Coffe.AnimateByUpdate = true;
            container.Coffe.Loop = false;
            container.Boil.ToCount = result.BoilMultiplier;
            container.Boil.AnimateByUpdate = true;
            container.Boil.Loop = false;
            container.Full.ToCount = result.ResultValue;
            container.Full.AnimateByUpdate = true;
            container.Full.Loop = false;
        }

        private void OnShowEnd(UniTaskAnimationLazyObject obj)
        {
            obj.AnimationCompleted -= OnShowEnd;
            obj.AnimationCompleted += OnAnimationComplete;
            obj.Play(true).Forget();
        }

        private void OnAnimationComplete(UniTaskAnimationLazyObject obj)
        {
            obj.AnimationCompleted -= OnAnimationComplete;
            _gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.CompareLevelComplete);
            Component.IsCoffeeShowing = false;
        }

        public void DisposeMechanic()
        {
        }
    }
}