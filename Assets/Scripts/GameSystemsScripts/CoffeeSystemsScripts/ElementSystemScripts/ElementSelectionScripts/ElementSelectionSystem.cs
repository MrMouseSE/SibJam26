using GameSystemsScripts.GameInputScripts;
using GameSystemsScripts.GameStateScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts
{
    public class ElementSelectionSystem : IGameSystem
    {
        public ElementSelectionComponent SelectionComponent;
        public ElementSelectionMechanic SelectionMechanic;

        public ElementSelectionSystem()
        {
            SelectionComponent = new ElementSelectionComponent();
            SelectionMechanic = new ElementSelectionMechanic(SelectionComponent);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
            var inputSystem = (GameInputSystem)gameSystemsHandler.GetGameSystem(typeof(GameInputSystem));
            if (!inputSystem.Component.MouseWasPressedThisFrame) return;
            SelectionMechanic.SelectionComponent.CurrentTouchedContainer =
                inputSystem.Component.MousePressedHitColliders[0].GetComponent<ElementContainer>();
            SelectionMechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}