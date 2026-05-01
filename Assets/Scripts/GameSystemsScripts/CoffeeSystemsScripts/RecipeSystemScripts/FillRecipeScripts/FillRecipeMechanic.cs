using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementSelectionScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts
{
    public class FillRecipeMechanic : IGameMechanic
    {
        public FillRecipeComponent Component;

        public FillRecipeMechanic(FillRecipeComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            var elementSystem = (ElementSelectionSystem)gameSystemsHandler.GetGameSystem(typeof(ElementSelectionSystem));
            if (elementSystem.SelectionComponent.CurrentTouchedContainer == null) return;
            var elementsDrawSystem = (ElementsDrawSystem)gameSystemsHandler.GetGameSystem(typeof(ElementsDrawSystem));
            if (Component.RecipeContainers.Contains(elementSystem.SelectionComponent.CurrentTouchedContainer))
            {
                Component.RecipeContainers.Remove(elementSystem.SelectionComponent.CurrentTouchedContainer);
                elementsDrawSystem.Component.DrawedElements.Add(elementSystem.SelectionComponent.CurrentTouchedContainer);
            }
            else
            {
                Component.RecipeContainers.Add(elementSystem.SelectionComponent.CurrentTouchedContainer);
                elementsDrawSystem.Component.DrawedElements.Remove(elementSystem.SelectionComponent.CurrentTouchedContainer);
            }
        }

        public void DisposeMechanic()
        {
        }
    }
}