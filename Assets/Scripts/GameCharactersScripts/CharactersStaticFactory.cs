using GameSystemsScripts.GameCharactersSystems.CharactersGroupSystem;
using GameSystemsScripts.GameCharactersSystems.CharacterSystem;

namespace GameCharactersScripts
{
    public class CharactersStaticFactory
    {
        public static CharacterComponent GenerateCharacterFromDescription(CharacterGenerationDescription description)
        {
            CharacterComponent component = new CharacterComponent();
            component.MaxHits = description.GetCurrentValue(description.MinMaxHits);
            component.MaxMorale = description.GetCurrentValue(description.MinMaxMorale);
            component.MaxDamage = description.GetCurrentValue(description.MinMaxDamage);
            component.MaxSlotAvailable = description.GetCurrentValue(description.CharacterMinMaxItemSlotsAvailable);
            component.CurrentHits = component.MaxHits;
            component.CurrentMorale = component.MaxMorale;
            component.CurrentDamage = component.MaxDamage;
            component.CurrentSlotAvailable = component.MaxSlotAvailable;
            return component;
        }

        public static CharacterGroupComponent GenerateCharacterGroup()
        {
            
        }
    }
}