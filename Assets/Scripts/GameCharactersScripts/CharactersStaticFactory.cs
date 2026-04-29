using GameSystemsScripts.GameCharactersSystems.CharactersGroupSystem;
using GameSystemsScripts.GameCharactersSystems.CharacterSystem;
using GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem;
using UnityEngine;

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

        public static CharacterGroupComponent GenerateCharacterRandomGroup(CharactersGenerationDescriptionsHolder description, int groupSize)
        {
            CharacterGroupComponent component = new CharacterGroupComponent();
            component.Characters = new CharacterComponent[groupSize];
            for (int i = 0; i < groupSize; i++)
            {
                component.Characters[i] = GenerateCharacterFromDescription(description.GetRandomCharacter());
            }
            return component;
        }

        public static CharactersGroupContainer CreateGlobalMapGroupContainer(CharactersGenerationDescriptionsHolder description)
        {
            return Object.Instantiate(description.GlobalMapToken);
        }
    }
}