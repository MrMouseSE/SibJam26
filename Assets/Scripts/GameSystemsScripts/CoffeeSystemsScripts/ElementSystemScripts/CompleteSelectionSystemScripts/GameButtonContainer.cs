using System;
using System.Threading;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;

namespace GameSystemsScripts.CoffeeSystemsScripts.ElementSystemScripts.CompleteSelectionSystemScripts
{
    public class GameButtonContainer : MonoBehaviour
    {
        public GameObject ButtonGameObject;
        public Transform ButtonTrasform;
        public Collider ButtonCollider;
        public AudioContainer AudioContainer;
        
        public TweenGroupAnimation ButtonActivateAnimations;
        public TweenGroupAnimation ClickAnimations;
        public TweenGroupAnimation HoverAnimations;
        
        public ButtonTooltipContainer ButtonTooltipContainer;
        
        public Action OnButtonPressed;
        
        private CancellationTokenSource _ctsActivation = new CancellationTokenSource();
        private CancellationTokenSource _ctsHover = new CancellationTokenSource();
        
        public float ClickAnimationDuration { get; set; }
        public float HoverAnimationDuration { get; set; }

        public void SetActive(bool active, float duration)
        {
            AudioContainer.Play(active ? SoundType.AppearSound : SoundType.DeathSound);
            ButtonCollider.enabled = active;
            new UniTaskAnimationLazyObject(ButtonActivateAnimations, ref _ctsActivation, duration, active).Play().Forget();
        }
        public void OnMouseUp()
        {
            AudioContainer.Play(SoundType.ActionSound);
            new UniTaskAnimationLazyObject(ClickAnimations, ref _ctsActivation, ClickAnimationDuration, true).Play().Forget();
            OnButtonPressed?.Invoke();
        }

        public void OnMouseEnter()
        {
            new UniTaskAnimationLazyObject(HoverAnimations, ref _ctsHover, HoverAnimationDuration, true).Play().Forget();
        }

        public void OnMouseExit()
        {
            new UniTaskAnimationLazyObject(HoverAnimations, ref _ctsHover, HoverAnimationDuration, false).Play().Forget();
        }
    }
}