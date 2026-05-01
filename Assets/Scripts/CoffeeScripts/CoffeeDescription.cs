using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(menuName = "Create CoffeeRecipesDescription", fileName = "CoffeeRecipesDescription", order = 0)]
    public class CoffeeDescription : ScriptableObject
    {
        public CoffeeRecipeDescription[] Recipes;
        public CoffeeElementDescription[] Elements;

        public CoffeeElementDescription GetRandomElementDescription()
        {
            return Elements[Random.Range(0, Elements.Length)];
        }
    }
}