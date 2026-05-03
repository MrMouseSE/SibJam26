using UnityEngine;
using UnityEngine.Serialization;

namespace CoffeeScripts
{
    [CreateAssetMenu(fileName = "Coffee/CoffeeElementDescription", menuName = "CoffeeElementDescription")]
    public class CoffeeElementDescription : ScriptableObject
    {
        public string ElementName;
        public Sprite Sprite;
        public Color BackColor;
        public AudioClip HoverSound;

        [FormerlySerializedAs("Type")] [Space]
        public ElementType ElementType;
        
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