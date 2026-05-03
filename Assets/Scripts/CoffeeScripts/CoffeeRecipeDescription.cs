using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(menuName = "Coffee/CoffeeRecipeDescription", fileName = "CoffeeRecipeDescription", order = 0)]
    public class CoffeeRecipeDescription : ScriptableObject
    {
        public string RecipeName;
        public string RecipeTitle;
        public Sprite CoffeeSprite;
        public List<ElementType> ElementTypes;
        public List<CoffeeElementDescription> ElementDescriptions;
        public float RecipeCost;
        public float RecipeMultiplier;

        private List<string> _recipeNames;

        private void OnValidate()
        {
            _recipeNames = ElementDescriptions.Select(element => element.name).ToList();
        }

        public bool CompareWithRecipe(List<ElementContainer> elements)
        {
            int elementsMatch = 0;
            foreach (var elementDescription in ElementDescriptions)
            {
                if(elements.Any(x=>x.ElementName == elementDescription.ElementName))
                    elementsMatch++;
            }
            bool isEelementsMatch = elementsMatch == ElementDescriptions.Count;

            List<ElementType> elementTypesRef = elements.Select(container => container.ElementType).ToList();

            bool isTypesMatch = true;
            foreach (var unused in ElementTypes.Where(elementType => !elementTypesRef.Remove(elementType)))
            {
                isTypesMatch = false;
            }
            return isEelementsMatch && isTypesMatch;
        }
    }
}