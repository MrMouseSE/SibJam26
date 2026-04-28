using UnityEngine;

namespace GameCharactersScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameSO/Create CharactersGenerationDescriptionsHolder",
        fileName = "CharactersGenerationDescriptionsHolder", order = 0)]
    public class CharactersGenerationDescriptionsHolder : ScriptableObject
    {
        
        public CharacterGenerationDifficultyDescription[] Characters;

        public CharacterGenerationDescription GetRandomCharacter()
        {
            var group = GetRandomDifficultyGroup();
            return group.CharactersDescriptions[Random.Range(0, group.CharactersDescriptions.Length)];
        }

        private CharacterGenerationDifficultyDescription GetRandomDifficultyGroup()
        {
            return Characters[Random.Range(0, Characters.Length)];
        }
    }
}