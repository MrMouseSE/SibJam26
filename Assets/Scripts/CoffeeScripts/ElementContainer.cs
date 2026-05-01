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
        public TweenAnimation ElementAppearAnimation;
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
        public float ElementVigorValue;
        public float ElementVigorMultiplier;
        public float ElementTesteValue;
        public float ElementTesteMultiplier;
        
        public AnimationsDescription AnimationsDescription { get; set; }
        private CancellationTokenSource _animationHoverCancellationToken = new CancellationTokenSource();
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            UniTaskAnimationObject newAnimationObject = new UniTaskAnimationObject();
            _animationHoverCancellationToken.Cancel();
            _animationHoverCancellationToken = new CancellationTokenSource();
            newAnimationObject.StartAnimation(ElementHoverAnimation, _animationHoverCancellationToken.Token,
                AnimationsDescription.ElementsAnimationDescription.ElementHoverAnimationDuration, true).Forget();
            SoundContainer.Play(SoundType.AppearSound);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _animationHoverCancellationToken.Cancel();
            SoundContainer.Play(SoundType.DeathSound);
        }
    }
}