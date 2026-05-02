using GameScripts;
using GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsRedrawScripts
{
    public class ElementsRedrawSystem : IGameSystem
    {
        public ElementsRedrawComponent Component;
        public ElementsRedrawMechanic Mechanic;

        public ElementsRedrawSystem()
        {
            Component = new ElementsRedrawComponent();
            Mechanic = new ElementsRedrawMechanic(Component);
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.SelectElements) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}