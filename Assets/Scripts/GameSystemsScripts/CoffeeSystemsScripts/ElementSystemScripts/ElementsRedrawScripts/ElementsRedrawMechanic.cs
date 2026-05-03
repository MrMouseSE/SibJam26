using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts;
using GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts;
using GameSystemsScripts.GameSpeedScripts;
using ScenesOperatingScripts;
using SupportScripts;
using TweenScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsRedrawScripts
{
    public class ElementsRedrawMechanic : IGameMechanic
    {
        public ElementsRedrawComponent Component;

        public ElementsRedrawMechanic(ElementsRedrawComponent component)
        {
            Component = component;
        }

        public void SetButtonContainer(GameButtonContainer buttonContainer, GameSystemsHandler gameSystemsHandler)
        {
            Component.RedrawButtonContainer = buttonContainer;
            Component.RedrawButtonContainer.OnButtonPressed += RedrawButtonPushed;
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            Component.RedrawButtonContainer.HoverAnimationDuration = speedSystem.Component.AnimationsDescription.HoverAnimationDuration;
            Component.RedrawButtonContainer.ClickAnimationDuration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
        }

        private void RedrawButtonPushed()
        {
            Component.IsRedrawButtonPressed = true;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsRedrawButtonPressed) return;
            var handSystem = (ElementsHandSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            var selectionSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            var speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));

            if (selectionSystem.Component.RecipeContainers.Count == 0) return;
            
            Component.IsRedrawButtonPressed = false;
            
            handSystem.Component.UnusedSwaps ++;
            if (handSystem.Component.UnusedSwaps == handSystem.Component.CurrentSwapCount)
            {
                var tooltipContainer = Component.RedrawButtonContainer.ButtonTooltipContainer;
                UniTaskAnimationLazyObject animTooltipObj = new(tooltipContainer.AppearAnimation, ref tooltipContainer.CancelToken,
                    speedSystem.Component.AnimationsDescription.ButtonTooltipAnimationDuration,true);
                animTooltipObj.Play().Forget();
                Component.RedrawButtonContainer.SetActive(false,
                    speedSystem.Component.AnimationsDescription.ActivateAnimationDuration);
            }
            
            foreach (var recipeContainer in selectionSystem.Component.RecipeContainers)
            {
                StaticElementFactory.SetValuesToContainer(recipeContainer);
                float duration =
                    speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectionAnimationDuration;
                
                UniTaskAnimationLazyObject animObj = new(recipeContainer.ElementSelectAnimation, ref recipeContainer.CancelToken,
                    duration, !recipeContainer.IsSelected);
                
                animObj.Play().Forget();
                recipeContainer.IsSelected = false;
            }
            selectionSystem.Component.RecipeContainers.Clear();
        }

        public void DisposeMechanic()
        {
        }
    }
}