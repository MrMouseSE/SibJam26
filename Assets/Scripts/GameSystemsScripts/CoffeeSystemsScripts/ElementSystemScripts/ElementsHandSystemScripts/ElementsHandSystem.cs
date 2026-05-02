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
            
        }

        public void Initialize(GameSystemsHandler gameSystemsHandler)
        {
            Mechanic.InitializeHandValues();
            Mechanic.FixHandCounts();
        }

        public void UpdateSystem(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            Mechanic.UpdateMechanic(gameSystemsHandler, deltaTime);
        }

        public void DisposeSystem()
        {
        }
    }
}