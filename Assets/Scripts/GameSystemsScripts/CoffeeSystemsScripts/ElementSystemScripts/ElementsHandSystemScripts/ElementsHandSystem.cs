using CoffeeScripts.ElementsInventoryScripts;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts
{
    public class ElementsHandSystem : IGameSystem
    {
        public ElementsHandComponent Component;
        public ElementsHandMechanic Mechanic;

        public ElementsHandSystem(ElementsHandDescription elementsHandDescription)
        {
            Component = new ElementsHandComponent(elementsHandDescription);
            Mechanic = new ElementsHandMechanic(Component);
            Mechanic.FixHandCounts();
        }
        
        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeSystem()
        {
        }
    }
}