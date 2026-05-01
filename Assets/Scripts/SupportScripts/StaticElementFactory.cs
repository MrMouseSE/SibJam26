using CoffeeScripts;
using UnityEngine;

namespace SupportScripts
{
    public static class StaticElementFactory
    {
        public static ElementContainer ElementPrefab;
        
        public static ElementContainer GetElementContainer(CoffeeDescription description)
        {
            var container = Object.Instantiate(ElementPrefab);
            var elementDescription = description.GetRandomElementDescription();
            container.ElementName = elementDescription.ElementName;
            container.Rarity = elementDescription.Rarity;
            container.Sprite = elementDescription.Sprite;
            container.SpriteRenderer.sprite = elementDescription.Sprite;
            container.ElementVigorValue = elementDescription.ElementVigorValue;
            container.ElementVigorMultiplier = elementDescription.ElementVigorMultiplier;
            container.ElementTesteValue = elementDescription.ElementTesteValue;
            container.ElementTesteMultiplier = elementDescription.ElementTesteMultiplier;
            return container;
        }
    }
}