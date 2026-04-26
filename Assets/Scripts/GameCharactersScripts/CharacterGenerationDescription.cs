using UnityEngine;

namespace GameCharactersScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameSO/Create CharacterGenerationDescription", fileName = "CharacterGenerationDescription", order = 0)]
    public class CharacterGenerationDescription : ScriptableObject
    {
        public Vector2Int CharacterMinMaxItemSlotsAvailable;
        public Sprite[] CharacterSprite;

        [Space]
        public Vector2 MinMaxMorale;
        public Vector2 MinMaxDamage;
        public Vector2 MinMaxHits;

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
    }
}