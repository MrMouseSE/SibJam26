using CoffeeScripts;

namespace SupportScripts
{
    public static class StaticElementFactory
    {
        public static CoffeeDescription CoffeeDescription;

        public static void SetValuesToContainer(ElementContainer container)
        {
            var elementDescription = CoffeeDescription.GetRandomElementDescription();
            container.ElementName = elementDescription.ElementName;
            container.Rarity = elementDescription.Rarity;
            container.Sprite = elementDescription.Sprite;
            container.SpriteRenderer.sprite = elementDescription.Sprite;
            container.ElementVigorValue = elementDescription.ElementVigorValue;
            container.ElementVigorMultiplier = elementDescription.ElementVigorMultiplier;
            container.ElementTesteValue = elementDescription.ElementTesteValue;
            container.ElementTesteMultiplier = elementDescription.ElementTesteMultiplier;
        }
    }
}