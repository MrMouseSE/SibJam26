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
            
        }

        public void DisposeMechanic()
        {
        }
    }
}