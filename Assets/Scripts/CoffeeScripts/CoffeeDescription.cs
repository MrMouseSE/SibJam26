using CoffeeScripts.ElementsInventoryScripts;
using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(menuName = "Coffee/CoffeeDescription", fileName = "CoffeeDescription", order = 0)]
    public class CoffeeDescription : ScriptableObject
    {
        public CoffeeRecipeDescription[] Recipes;
        public CoffeeElementDescription[] Elements;
        public ElementsHandDescription ElementsHand;

        public CoffeeElementDescription GetRandomElementDescription()
        {
            return Elements[Random.Range(0, Elements.Length)];
        }
    }
}