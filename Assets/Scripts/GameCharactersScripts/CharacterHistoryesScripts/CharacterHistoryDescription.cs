using UnityEngine;

namespace GameCharactersScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameSO/Create CharacterHistoryDescription", fileName = "CharacterHistoryDescription", order = 0)]
    public class CharacterHistoryDescription : ScriptableObject
    {
        public string CharacterName;
        public Sprite CharacterSprite;
        public string CharacterHistory;
    }
}