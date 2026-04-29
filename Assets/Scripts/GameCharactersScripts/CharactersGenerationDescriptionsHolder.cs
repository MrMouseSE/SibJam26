using GameSystemsScripts.GlobalMapSystems.GlobalMapCharactersGroupSystem;
using UnityEngine;

namespace GameCharactersScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameSO/Create CharactersGenerationDescriptionsHolder",
        fileName = "CharactersGenerationDescriptionsHolder", order = 0)]
    public class CharactersGenerationDescriptionsHolder : ScriptableObject
    {
        public CharactersGroupContainer GlobalMapToken;
        public CharacterGenerationDifficultyDescription[] Characters;

        public CharacterGenerationDescription GetRandomCharacter()
        {
            var group = GetRandomGroup();
            return group.CharactersDescriptions[Random.Range(0, group.CharactersDescriptions.Length)];
        }

        private CharacterGenerationDifficultyDescription GetRandomGroup()
        {
            return Characters[Random.Range(0, Characters.Length)];
        }
    }
}