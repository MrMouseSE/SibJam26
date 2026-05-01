using UnityEngine;

namespace CoffeeScripts
{
    [CreateAssetMenu(fileName = "CoffeeElementDescription", menuName = "Scriptable Objects/CoffeeElementDescription")]
    public class CoffeeElementDescription : ScriptableObject
    {
        public string ElementName;
        public Sprite Sprite;

        [Space]
        public ElementRarity rarity;
        public float ElementCoast;
        public float ElementMultiplier;
    }
}