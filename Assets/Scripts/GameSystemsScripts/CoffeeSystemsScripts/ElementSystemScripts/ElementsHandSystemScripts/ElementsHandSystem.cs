using System;
using CoffeeScripts.ElementsInventoryScripts;
using ScenesOperatingScripts;
using UnityEngine;

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

    public class ElementsHandComponent
    {
        public int CurrentElementsHandCapacity;
        public int CurrentElementsSwapCount;
        public ElementsHandDescription ElementsHandDescription;

        public ElementsHandComponent(ElementsHandDescription elementsHandDescription)
        {
            ElementsHandDescription = elementsHandDescription;
            CurrentElementsHandCapacity = PlayerPrefs.GetInt("ElementsHandCapacity", 0);
            CurrentElementsSwapCount = PlayerPrefs.GetInt("ElementsHandSwapCount", 0);
        }
    }
}