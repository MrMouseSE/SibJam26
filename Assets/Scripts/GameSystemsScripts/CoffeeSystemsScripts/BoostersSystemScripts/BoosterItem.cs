using System.Collections.Generic;
using System.Linq;
using CoffeeScripts;

namespace GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts
{
    public class BoosterItem
    {
        public string BoosterName;
        public float Multiplier = 1f;
        public bool IsEnabled = true;
        public List<string> RequiredElementNames = new();
        public List<ElementType> RequiredElementTypes = new();

        public float GetAppliedMultiplier(IReadOnlyList<ElementContainer> recipeContainers)
        {
            if (!IsEnabled)
            {
                return 1f;
            }

            HashSet<string> recipeElementNames = recipeContainers.Select(x => x.ElementName).ToHashSet();

            if (RequiredElementNames.Any(requiredElementName => !recipeElementNames.Contains(requiredElementName)))
            {
                return 1f;
            }

            HashSet<ElementType> recipeElementTypes = recipeContainers.Select(x => x.ElementType).ToHashSet();

            if (RequiredElementTypes.Any(requiredElementType => !recipeElementTypes.Contains(requiredElementType)))
            {
                return 1f;
            }

            return Multiplier;
        }
    }
}
