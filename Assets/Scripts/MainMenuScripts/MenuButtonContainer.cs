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

        private void OnPushAnimationCompleted(UniTaskAnimationObject obj)
        {
            obj.AnimationCompleted -= OnPushAnimationCompleted;
            OnButtonPushed?.Invoke();
        }
        
        private UniTaskAnimationObject StartUniTaskAnimationProcess(TweenGroupAnimation animations, float time, bool isForward)
        {
            
            _animationCancellationToken.Cancel();
            _animationCancellationToken = new CancellationTokenSource();
            UniTaskAnimationObject uniTaskAnimationObject = new();
            uniTaskAnimationObject.StartAnimation(animations, _animationCancellationToken.Token, time, isForward)
                .Forget();
            return uniTaskAnimationObject;
        }

        private void LoopIdleAnimation(UniTaskAnimationObject obj)
        {
            _ = obj.StartAnimation(AnimationIdleGroup, _animationCancellationToken.Token, IdleAnimationTime, true);
        }
    }
}