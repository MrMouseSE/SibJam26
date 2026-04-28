using UnityEngine;

namespace GameCharactersScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameSO/Create CharacterGenerationDescription", fileName = "CharacterGenerationDescription", order = 0)]
    public class CharacterGenerationDescription : ScriptableObject
    {
        public GameCharacterContainer CharacterContainer;
        public Vector2Int CharacterMinMaxItemSlotsAvailable;
        [Space]
        public Vector2 MinMaxHits;
        public Vector2 MinMaxMorale;
        public Vector2 MinMaxDamage;
        public Vector2Int CharacterMinMaxItemSlotsAvailable;

        [Space]
        public CharacterHistoryDescription[] CharacterHistoryes;

        public float GetCurrentValue(Vector2 value)
        {
            return Random.Range(value.x, value.y);
        }

        public int GetCurrentValue(Vector2Int value)
        {
            return Random.Range(value.x, value.y+1);
        }

        public CharacterHistoryDescription GetRandomCharacterHistoryDescription()
        {
            return CharacterHistoryes[Random.Range(0, CharacterHistoryes.Length)];
        }
    }
}