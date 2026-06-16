using GameSystemsScripts.CoffeeSystemsScripts.BoostersSystemScripts;

namespace GameObjectFactories
{
    public static class BoosterItemFactory
    {
        public static BoosterItem CreateFromDescription(BoosterDescription description)
        {
            return new BoosterItem
            {
                BoosterName = description.BoosterName,
                IsEnabled = description.IsEnabled,
                Multiplier = description.Multiplier,
                RequiredElementNames = description.RequiredElementNames,
                RequiredElementTypes = description.RequiredElementTypes
            };
        }
    }
}
