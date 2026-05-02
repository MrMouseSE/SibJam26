using CoffeeScripts;
using GameScripts;
using GameSystemsScripts.GameInputScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementSelectionScripts
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

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
            var inputSystem = (GameInputSystem)gameSystemsHandler.GetGameSystem(typeof(GameInputSystem));
            if (!inputSystem.Component.MouseWasPressedThisFrame) return;
            if (inputSystem.Component.MousePressedHitColliders.Count == 0) return;
            SelectionMechanic.Component.CurrentTouchedContainer =
                inputSystem.Component.MousePressedHitColliders[0].GetComponent<ElementContainer>();
            SelectionMechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}