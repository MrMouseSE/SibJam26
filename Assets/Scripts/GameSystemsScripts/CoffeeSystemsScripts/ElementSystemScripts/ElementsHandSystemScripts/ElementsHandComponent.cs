using CoffeeScripts.ElementsInventoryScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts
{
    public class ElementsHandComponent
    {
        public bool IsValuesUpdatNeed;
        public int AddDrawCapacityValue;
        public int AddElementSwapCountValue;
        
        public int CurrentElementsToDrawCapacity;
        public int CurrentElementsSwapCount;
        public int UnusedSwaps;
        public ElementsHandDescription ElementsHandDescription;

        public ElementsHandComponent(ElementsHandDescription elementsHandDescription)
        {
            ElementsHandDescription = elementsHandDescription;
            CurrentElementsToDrawCapacity = PlayerPrefs.GetInt("ElementsHandCapacity", 0);
            CurrentElementsSwapCount = PlayerPrefs.GetInt("ElementsHandSwapCount", 0);
        }
    }
}