using System;
using System.Threading;
using SoundsComponentsScripts;
using TweenScripts;
using UnityEngine;

namespace MainMenuScripts
{
    public class MenuButtonContainer : MonoBehaviour
    {
        public float IdleAnimationTime;
        public TweenGroupAnimation AnimationIdleGroup;
        public float PushAnimationTime;
        public TweenGroupAnimation AnimationPushGroup;

        [Space] public AudioContainer ButtonSounds;

        public Action OnButtonPushed;

        private bool _isButtonPushed;

        private CancellationTokenSource _animationCancellationToken = new();

        private void OnMouseEnter()
        {
            if (_isButtonPushed) return;
            ButtonSounds.Play(SoundType.ActionSound);
            var animationObject = StartUniTaskAnimationProcess(AnimationIdleGroup, IdleAnimationTime, true);
            animationObject.AnimationCompleted += LoopIdleAnimation;
        }

        private void OnMouseExit()
        {
            _animationCancellationToken?.Cancel();
            AnimationIdleGroup.SetForceState(true);
        }

        private void OnMouseDown()
        {
            _isButtonPushed = true;
            ButtonSounds.Play(SoundType.AppearSound);
            StartUniTaskAnimationProcess(AnimationPushGroup, PushAnimationTime, true);
        }

        private void OnMouseUp()
        {
            _isButtonPushed = false;
            ButtonSounds.Play(SoundType.DeathSound);
            var animationObject = StartUniTaskAnimationProcess(AnimationPushGroup, PushAnimationTime, false);
            animationObject.AnimationCompleted += OnPushAnimationCompleted;
        }

        private void OnPushAnimationCompleted(UniTaskAnimationLazyObject obj)
        {
            obj.AnimationCompleted -= OnPushAnimationCompleted;
            OnButtonPushed?.Invoke();
        }
        
        private UniTaskAnimationLazyObject StartUniTaskAnimationProcess(TweenGroupAnimation animations, float time, bool isForward)
        {
            UniTaskAnimationLazyObject animObj = new (animations, ref _animationCancellationToken, time, isForward);
            animObj.Play().Forget();
            return animObj;
        }

        private void LoopIdleAnimation(UniTaskAnimationLazyObject obj)
        {
            _ = obj.Play();
        }
    }
}