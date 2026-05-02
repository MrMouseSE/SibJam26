using System;
using ScenesOperatingScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts
{
    public class ElementsHandMechanic : IGameMechanic
    {
        public ElementsHandComponent Component;

        public ElementsHandMechanic(ElementsHandComponent component)
        {
            Component = component;
        }

        public void FixHandCounts()
        {
            Component.CurrentElementsToDrawCapacity = Math.Clamp(Component.CurrentElementsToDrawCapacity, Component.ElementsHandDescription.MinMaxElementsHandCapacity.x, 
                Component.ElementsHandDescription.MinMaxElementsHandCapacity.y);
            Component.CurrentElementsSwapCount = Math.Clamp(Component.CurrentElementsSwapCount, Component.ElementsHandDescription.MinMaxElementSwapCount.x, 
                Component.ElementsHandDescription.MinMaxElementSwapCount.y);
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsValuesUpdatNeed) return;
            Component.IsValuesUpdatNeed = false;
            Component.CurrentElementsToDrawCapacity += Component.AddDrawCapacityValue;
            Component.AddDrawCapacityValue = 0;
            Component.CurrentElementsSwapCount += Component.AddElementSwapCountValue;
            Component.AddElementSwapCountValue = 0;
            FixHandCounts();
        }

        public void DisposeMechanic()
        {
        }
    }
}