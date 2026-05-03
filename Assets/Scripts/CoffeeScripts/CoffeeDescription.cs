using System.Collections.Generic;
using System.Linq;
using CoffeeScripts.ElementsInventoryScripts;
using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(menuName = "Coffee/CoffeeDescription", fileName = "CoffeeDescription", order = 0)]
    public class CoffeeDescription : ScriptableObject
    {
        public int StartElementCount;
        public List<CoffeeElementDescription> StartAvailableElements;
        
        public CoffeeRecipeDescription DefaultRecipe;
        
        public CoffeeRecipeDescription[] Recipes;
        public List<CoffeeElementDescription> Elements;
        public ElementsHandDescription ElementsHand;

        public CoffeeElementDescription GetRandomElementDescription()
        {
            var elems = ElementsStaticInventory.CurrentAvailableElements.Where(x => x.Value > 0);
            var elementsAvailableList = elems.ToList();
            int index = Random.Range(0, elementsAvailableList.Count);
            string elementName = elementsAvailableList[index].Key;
            return Elements.FirstOrDefault(x => x.ElementName == elementName);
        }
    }
}