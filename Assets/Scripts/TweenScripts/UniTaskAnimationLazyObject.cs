using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TweenScripts
{
    public class UniTaskAnimationLazyObject
    {
        public Action<UniTaskAnimationLazyObject> AnimationCompleted;
        
        private readonly TweenAnimation _tweenAnimation;
        private readonly CancellationTokenSource _tokenSourceSource;
        private readonly float _time;
        private readonly bool _isForward;
        
        public UniTaskAnimationLazyObject(TweenAnimation tweenAnimation, ref CancellationTokenSource tokenSource, float time, bool isForward)
        {
            _tweenAnimation = tweenAnimation;
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            _tokenSourceSource = tokenSource;
            _time = time;
            _isForward = isForward;
        }
        
        public async UniTaskVoid Play(bool isForwardOverride = false)
        {
            float animationTime = _time;
            isForwardOverride = !isForwardOverride && _isForward;
            float baseValue = isForwardOverride ? 1f : 0;
            float mult = isForwardOverride ? -1f : 1f;
            
            while (animationTime > 0f)
            {
                animationTime -= Time.deltaTime;
                float evaluateTime = baseValue + mult * animationTime / _time;
                
                _tweenAnimation.Evaluate(evaluateTime);
                await UniTask.Yield(cancellationToken: _tokenSourceSource.Token, true);
            }
            AnimationCompleted?.Invoke(this);
        }
    }
}