using CoffeeScripts.ElementsInventoryScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts
{
    public class ElementsHandComponent
    {
        public bool IsValuesUpdateNeed;
        public int AddHandSlotCapacityValue;
        public int AddSwapCountValue;
        
        public int CurrentHandSlotCapacity;
        public int CurrentSwapCount;
        public int UnusedSwaps;
        public ElementsHandDescription ElementsHandDescription;

        public ElementsHandComponent(ElementsHandDescription elementsHandDescription)
        {
            ElementsHandDescription = elementsHandDescription;
        }
    }
}