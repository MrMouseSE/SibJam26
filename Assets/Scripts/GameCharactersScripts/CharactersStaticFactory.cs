using GameSystemsScripts.GameCharactersSystems.CharactersGroupSystem;
using GameSystemsScripts.GameCharactersSystems.CharacterSystem;
using UnityEngine;

namespace GameCharactersScripts
{
    public static class CharactersStaticFactory
    {
        private static CharactersGenerationDescriptionsHolder _characters;

        public static void SetDescription(CharactersGenerationDescriptionsHolder characters)
        {
            _characters = characters;
        }
        
        public static CharacterComponent GenerateCharacterFromDescription(CharacterGenerationDescription description)
        {
            CharacterComponent component = new CharacterComponent();
            component.Container = Object.Instantiate(description.CharacterContainer);
            component.CharacterHistoryDescription = description.GetRandomCharacterHistoryDescription();
            component.Container.CharacterSprite = component.CharacterHistoryDescription.CharacterSprite;
            component.Container.CharacterName.text = component.CharacterHistoryDescription.CharacterName;
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

        public static CharacterGroupComponent GenerateRandomGroup()
        {
            int groupSize = Random.Range(_characters.)
            CharacterGroupComponent groupComponent = new CharacterGroupComponent();
            groupComponent.Characters = new CharacterComponent[groupSize];
            for (int i = 0; i < groupSize; i++)
            {
                groupComponent.Characters[i] = GenerateCharacterFromDescription(_characters.GetRandomCharacter());
            }
            return groupComponent;
        }
    }
}