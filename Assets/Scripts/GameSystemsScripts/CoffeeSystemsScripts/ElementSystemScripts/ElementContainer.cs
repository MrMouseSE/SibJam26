using System.Threading;
using CoffeeScripts;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts
{
    public class ElementContainer : MonoBehaviour
    {
        public GameObject ElementPrefab;
        public Transform ElementContainerTransform;
        public TweenAnimation ElementAnimation;
        public SoundContainer SoundContainer;
        
        public BoxCollider ElementCollider;
        public SpriteRenderer SpriteRenderer;
        
        public CancellationTokenSource AnimationCancellationToken;

        [HideInInspector]
        public bool IsSelected;
        
        [HideInInspector]
        public string ElementName;
        [HideInInspector]
        public Sprite Sprite;

        [Space]
        public ElementRarity Rarity;
        public float ElementCoast;
        public float ElementMultiplier;
    }
}