using System;
using System.Threading;
using TweenScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class ElementDrawStateButtonContainer : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject ButtonGameObject;
        public Transform ButtonTrasform;
        
        public TweenGroupAnimation ButtonActivateAnimations;
        public TweenGroupAnimation ClickAnimations;
        public TweenGroupAnimation HoverAnimations;
        
        public Action OnButtonPressed;
        
        private CancellationTokenSource _ctsActivation = new CancellationTokenSource();
        private CancellationTokenSource _ctsHover = new CancellationTokenSource();
        
        public float ClickAnimationDuration { get; set; }
        public float HoverAnimationDuration { get; set; }

        public void SetActive(bool active, float duration)
        {
            new UniTaskAnimationLazyObject(ButtonActivateAnimations, ref _ctsActivation, duration, active).Play().Forget();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            new UniTaskAnimationLazyObject(ClickAnimations, ref _ctsActivation, ClickAnimationDuration, true).Play().Forget();
            OnButtonPressed?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            new UniTaskAnimationLazyObject(HoverAnimations, ref _ctsHover, HoverAnimationDuration, true).Play().Forget();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            new UniTaskAnimationLazyObject(HoverAnimations, ref _ctsHover, HoverAnimationDuration, false).Play().Forget();
        }
    }
}