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

        private void RedrawButtonPushed()
        {
            Component.IsRedrawButtonPressed = true;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsRedrawButtonPressed) return;
            
            Component.IsRedrawButtonPressed = false;
            var handSystem = (ElementsHandSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsHandSystem));
            handSystem.Component.UnusedSwaps --;
            if (handSystem.Component.UnusedSwaps == 0)
            {
                Component.RedrawButtonContainer.SetActive(false);
            }
            //TODO: redraw selected elements
            FillRecipeSystem selectionSystem = (FillRecipeSystem)gameSystemsHandler.GetGameSystem(typeof(FillRecipeSystem));
            GameSpeedSystem speedSystem = (GameSpeedSystem)gameSystemsHandler.GetGameSystem(typeof(GameSpeedSystem));
            foreach (var recipeContainer in selectionSystem.Component.RecipeContainers)
            {
                StaticElementFactory.SetValuesToContainer(recipeContainer);
                UniTaskAnimationObject uniTaskAnimationObject = new UniTaskAnimationObject();
                uniTaskAnimationObject.StartContainerSelectionAnimation(recipeContainer,
                    speedSystem.Component.AnimationsDescription.ElementsAnimationDescription.ElementSelectionAnimationDuration).Forget();
                recipeContainer.IsSelected = false;
            }
            selectionSystem.Component.RecipeContainers.Clear();
        }

        public void DisposeMechanic()
        {
        }
    }
}