using UnityEngine;

namespace GameCharactersScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameSO/Create CharactersHistoriesLibrary", fileName = "CharactersHistoriesLibrary", order = 0)]
    public class CharactersHistoriesLibrary : ScriptableObject
    {
        public CharacterHistoryDescription[] CharacterHistories;
    }
}