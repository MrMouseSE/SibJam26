using System;
using System.Threading;
using CoffeeScripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TweenScripts
{
    public class UniTaskAnimationObject
    {
        public Action<UniTaskAnimationObject> AnimationCompleted;
        
        public async UniTaskVoid StartAnimation(TweenAnimation[] tweenAnimations, CancellationToken token, float time, bool isForward)
        {
            float animationTime = time;
            float baseValue = isForward ? 1f : 0;
            float mult = isForward ? -1f : 1f;
            
            while (animationTime > 0f)
            {
                animationTime -= Time.deltaTime;
                float evaluateTime = baseValue + mult * animationTime / time;
                
                foreach (var tweenAnimation in tweenAnimations)
                {
                    tweenAnimation.Evaluate(evaluateTime);
                }
                await UniTask.Yield(cancellationToken: token, true);
            }
            AnimationCompleted?.Invoke(this);
        }
        
        public async UniTaskVoid StartAnimation(TweenAnimation tweenAnimation, CancellationToken token, float time, bool isForward)
        {
            float animationTime = time;
            float baseValue = isForward ? 1f : 0;
            float mult = isForward ? -1f : 1f;
            
            while (animationTime > 0f)
            {
                animationTime -= Time.deltaTime;
                float evaluateTime = baseValue + mult * animationTime / time;
                
                tweenAnimation.Evaluate(evaluateTime);
                await UniTask.Yield(cancellationToken: token, true);
            }
            AnimationCompleted?.Invoke(this);
        }

        public async UniTaskVoid StartContainerSelectionAnimation(ElementContainer container, float time)
        {
            float animationTime = time;
            float baseValue = container.IsSelected ? 1f : 0;
            float mult = container.IsSelected ? -1f : 1f;

            while (animationTime > 0f)
            {
                animationTime -= Time.deltaTime;
                float evaluateTime = baseValue + mult * animationTime / time;

                container.ElementSelectAnimation.Evaluate(evaluateTime);
                await UniTask.Yield(cancellationToken: container.AnimationCancellationToken.Token, true);
            }

            AnimationCompleted?.Invoke(this);
        }
    }
}
