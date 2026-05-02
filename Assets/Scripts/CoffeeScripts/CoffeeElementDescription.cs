using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(fileName = "Coffee/CoffeeElementDescription", menuName = "CoffeeElementDescription")]
    public class CoffeeElementDescription : ScriptableObject
    {
        public string ElementName;
        public Sprite Sprite;

        [Space]
        public ElementRarity Rarity;
        public float ElementVigorValue;
        public float ElementVigorMultiplier;
        public float ElementTesteValue;
        public float ElementTesteMultiplier;

        [Space]
        public float DropChanceValue;
    }
}