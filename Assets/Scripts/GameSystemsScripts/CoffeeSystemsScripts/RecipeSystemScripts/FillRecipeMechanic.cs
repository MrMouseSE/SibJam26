using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts
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
            if (Component.RecipeContainers.Contains(elementSystem.SelectionComponent.CurrentTouchedContainer))
            {
                Component.RecipeContainers.Remove(elementSystem.SelectionComponent.CurrentTouchedContainer);
            }
            else
            {
                Component.RecipeContainers.Add(elementSystem.SelectionComponent.CurrentTouchedContainer);
            }
        }

        public void DisposeMechanic()
        {
        }
    }
}