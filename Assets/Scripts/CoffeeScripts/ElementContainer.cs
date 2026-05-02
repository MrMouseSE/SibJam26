using System.Threading;
using AnimationDescriptionsScripts;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CoffeeScripts
{
    public class ElementContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        
        public CancellationTokenSource CancelToken;

        [HideInInspector]
        public bool IsSelected;

        [HideInInspector]
        public bool IsUsedInGame;
        
        [HideInInspector]
        public string ElementName;
        [HideInInspector]
        public Sprite Sprite;
        
        public ElementRarity Rarity {get;set;}
        public float ElementVigorValue {get;set;}
        public float ElementVigorMultiplier {get;set;}
        public float ElementTesteValue {get;set;}
        public float ElementTesteMultiplier {get;set;}
        
        public AnimationsDescription AnimationsDescription { get; set; }
        private CancellationTokenSource _animCancelToken = new CancellationTokenSource();
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            UniTaskAnimationLazyObject newAnimObj = new UniTaskAnimationLazyObject(ElementHoverAnimation, ref _animCancelToken,
                AnimationsDescription.ElementsAnimationDescription.ElementHoverAnimationDuration, true);
            newAnimObj.Play().Forget();
            SoundContainer.Play(SoundType.AppearSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _animCancelToken.Cancel();
            SoundContainer.Play(SoundType.AppearSound);
        }
    }
}