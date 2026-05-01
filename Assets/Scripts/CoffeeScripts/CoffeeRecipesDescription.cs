using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(menuName = "Create CoffeeRecipesDescription", fileName = "CoffeeRecipesDescription", order = 0)]
    public class CoffeeRecipesDescription : ScriptableObject
    {
        public CoffeeRecipeDescription[] Recipes;
    }
}