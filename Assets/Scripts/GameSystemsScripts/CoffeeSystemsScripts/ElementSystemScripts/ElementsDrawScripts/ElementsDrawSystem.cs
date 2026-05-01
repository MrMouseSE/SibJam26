using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts
{
    public class ElementsDrawSystem : IGameSystem
    {
        public ElementsDrawComponent Component;
        public ElementsDrawMechanic Mechanic;

        public ElementsDrawSystem(GameSystemsHandler gameSystemsHandler)
        {
            Component = new ElementsDrawComponent();
            Mechanic = new ElementsDrawMechanic(Component, gameSystemsHandler);
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (gameSystemsHandler.StateSystem.Component.CurrentGameState != GameStates.DrawElements) return;
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}