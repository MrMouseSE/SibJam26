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

        public void AddHandElementsCapacity(int capacity)
        {
            Component.CurrentElementsHandCapacity = capacity;
            FixHandCounts();
        }

        public void AddSwapCount(int count)
        {
            Component.CurrentElementsSwapCount += count;
            FixHandCounts();
        }

        public void FixHandCounts()
        {
            Component.CurrentElementsHandCapacity = Math.Clamp(Component.CurrentElementsHandCapacity, Component.ElementsHandDescription.MinMaxElementsHandCapacity.x, 
                Component.ElementsHandDescription.MinMaxElementsHandCapacity.y);
            Component.CurrentElementsSwapCount = Math.Clamp(Component.CurrentElementsSwapCount, Component.ElementsHandDescription.MinMaxElementSwapCount.x, 
                Component.ElementsHandDescription.MinMaxElementSwapCount.y);
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
        }

        public void DisposeMechanic()
        {
        }
    }
}