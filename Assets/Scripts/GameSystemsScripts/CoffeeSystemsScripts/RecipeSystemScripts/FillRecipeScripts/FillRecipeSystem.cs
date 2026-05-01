using GameScripts;
using GameSystemsScripts.GameInputScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.RecipeSystemScripts.FillRecipeScripts
{
    public class FillRecipeSystem : IGameSystem
    {
        public FillRecipeComponent Component;
        public FillRecipeMechanic Mechanic;

        public FillRecipeSystem()
        {
            Component = new FillRecipeComponent();
            Mechanic = new FillRecipeMechanic(Component);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
            var inputSystem = (GameInputSystem)gameSystemsHandler.GetGameSystem(typeof(GameInputSystem));
            if (!inputSystem.Component.MouseWasPressedThisFrame) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
} 