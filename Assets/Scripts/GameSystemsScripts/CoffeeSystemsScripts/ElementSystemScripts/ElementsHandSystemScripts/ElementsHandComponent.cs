using CoffeeScripts.ElementsInventoryScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.ElementsHandSystemScripts
{
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