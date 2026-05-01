using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(menuName = "Create CoffeeRecipeDescription", fileName = "CoffeeRecipeDescription", order = 0)]
    public class CoffeeRecipeDescription : ScriptableObject
    {
        public string RecipeName;
        public CoffeeElementDescription[] ElementDescriptions;
        public float EffectMultiplier;
    }
}