using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(menuName = "Create CoffeeRecipeDescription", fileName = "CoffeeRecipeDescription", order = 0)]
    public class CoffeeRecipeDescription : ScriptableObject
    {
        public string RecipeName;
        public List<CoffeeElementDescription> ElementDescriptions;
        public float RecipeMultiplier;

        private List<string> _recipeNames;
        private void OnValidate()
        {
            _recipeNames = ElementDescriptions.Select(element => element.name).ToList();
        }

        public bool CompareWithRecipe(List<ElementContainer> elements)
        {
            List<string> elementsNames = elements.Select(element => element.name).ToList();
            return elementsNames.All(elementName => _recipeNames.Contains(elementName));
        }
    }
}