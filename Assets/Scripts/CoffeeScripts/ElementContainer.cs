using System.Threading;
using AnimationDescriptionsScripts;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;

namespace CoffeeScripts
{
    public class ElementContainer : MonoBehaviour
    {
        public GameObject ElementPrefab;
        public Transform ElementContainerTransform;
        public TweenAnimation ElementSelectAnimation;
        public TweenAnimation ElementIdleAnimation;
        public TweenAnimation ElementHoverAnimation;
        public TweenAnimation ElementDisappearAnimation;
        public SoundContainer SoundContainer;
        
        public BoxCollider ElementCollider;
        public SpriteRenderer SpriteRenderer;
        public SpriteRenderer BackSpriteRenderer;
        
        public CancellationTokenSource CancelToken = new();

        [HideInInspector]
        public bool IsSelected;

        [HideInInspector]
        public bool IsUsedInGame;
        
        [HideInInspector]
        public string ElementName;
        [HideInInspector]
        public Sprite Sprite;
        
        public ElementType ElementType;
        public ElementRarity Rarity {get;set;}
        public float ElementVigorValue {get;set;}
        public float ElementVigorMultiplier {get;set;}
        public float ElementTesteValue {get;set;}
        public float ElementTesteMultiplier {get;set;}
        
        public AnimationsDescription AnimationsDescription { get; set; }
        private CancellationTokenSource _animCancelToken = new();
        
        public void OnMouseEnter()
        {
            UniTaskAnimationLazyObject animObj = new UniTaskAnimationLazyObject(ElementHoverAnimation, ref _animCancelToken,
                AnimationsDescription.ElementsAnimationDescription.ElementHoverAnimationDuration, true);
            animObj.Play().Forget();
            SoundContainer.Play(SoundType.AppearSound);
        }

        public void OnMouseExit()
        {
            _animCancelToken.Cancel();
        }
    }
}