using System;
using ScenesOperatingScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts
{
    public class ElementsHandMechanic : IGameMechanic
    {
        public ElementsHandComponent Component;

        public ElementsHandMechanic(ElementsHandComponent component)
        {
            Component = component;
        }

        public void InitializeHandValues()
        {
            Component.CurrentHandSlotCapacity = PlayerPrefs.GetInt("ElementsHandCapacity", 0);
            Component.CurrentSwapCount = PlayerPrefs.GetInt("ElementsHandSwapCount", 2);
        }

        public void FixHandCounts()
        {
            Component.CurrentHandSlotCapacity = Math.Clamp(Component.CurrentHandSlotCapacity, Component.ElementsHandDescription.MinMaxElementsHandCapacity.x, 
                Component.ElementsHandDescription.MinMaxElementsHandCapacity.y);
            Component.CurrentSwapCount = Math.Clamp(Component.CurrentSwapCount, Component.ElementsHandDescription.MinMaxElementSwapCount.x, 
                Component.ElementsHandDescription.MinMaxElementSwapCount.y);
        }

        public void UpdateMechanic(GameSystemsHandler gameSystemsHandler, float deltaTime)
        {
            if (!Component.IsValuesUpdateNeed) return;
            Component.IsValuesUpdateNeed = false;
            Component.CurrentHandSlotCapacity += Component.AddHandSlotCapacityValue;
            Component.AddHandSlotCapacityValue = 0;
            Component.CurrentSwapCount += Component.AddSwapCountValue;
            Component.AddSwapCountValue = 0;
            FixHandCounts();
        }

        public void DisposeMechanic()
        {
        }
    }
}