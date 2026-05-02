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
            Component.RedrawButtonContainer.OnButtonPressed += RedrawButtonPushed;
        }
        
        public void InitializeButtonsValues(GameSystemsHandler gameSystemsHandler)
        {
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            Component.RedrawButtonContainer.HoverAnimationDuration = speedSystem.Component.AnimationsDescription.ButtonHoverAnimationDuration;
            Component.RedrawButtonContainer.ClickAnimationDuration = speedSystem.Component.AnimationsDescription.ClickAnimationsDuration;
        }

        private void RedrawButtonPushed()
        {
            Component.IsRedrawButtonPressed = true;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsRedrawButtonPressed) return;
            ElementsHandSystem handSystem = (ElementsHandSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            FillRecipeSystem selectionSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            
            Component.IsRedrawButtonPressed = false;
            
            handSystem.Component.UnusedSwaps --;
            if (handSystem.Component.UnusedSwaps == 0)
            {
                Component.RedrawButtonContainer.SetActive(false,
                    speedSystem.Component.AnimationsDescription.ButtonAnimationDuration);
            }
            
            foreach (var recipeContainer in selectionSystem.Component.RecipeContainers)
            {
                StaticElementFactory.SetValuesToContainer(recipeContainer);
                float duration =
                    speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectionAnimationDuration;
                
                UniTaskAnimationLazyObject animObj = new(recipeContainer.ElementSelectAnimation, ref recipeContainer.CancelToken,
                    duration, recipeContainer.IsSelected);
                
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