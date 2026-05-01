using GameScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsDrawScripts
{
    public class ElementsDrawMechanic : IGameMechanic
    {
        public ElementsDrawComponent Component;

        public ElementsDrawMechanic(ElementsDrawComponent component)
        {
            Component = component;
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            //TODO: Draw action
            gameSystemsHandler.StateSystem.Mechanic.ChangeState(GameStates.SelectElements);
        }

        public void DisposeMechanic()
        {
        }
    }
}