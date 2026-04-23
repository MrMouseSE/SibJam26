using System.Threading;
using Cysharp.Threading.Tasks;
using TweenScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainMenuScripts.SettingsHanlderScripts
{
    public class SettingsHandlerContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public float FoldTime;
        public float UnfoldTime;
        
        public TweenAnimation[] Animations;

        private int _animatingProcessState;
        private CancellationTokenSource _animationCancellationToken = new();

        private void Awake()
        {
            foreach (var tweenAnimation in Animations)
            {
                tweenAnimation.SetForceState(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_animatingProcessState == 1) return;
            _animatingProcessState = 1;
            _animationCancellationToken.Cancel();
            _animationCancellationToken = new CancellationTokenSource();
            StartFoldAnimation(_animationCancellationToken.Token, UnfoldTime, true).Forget();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_animatingProcessState == -1) return;
            _animatingProcessState = -1;
            _animationCancellationToken.Cancel();
            _animationCancellationToken = new CancellationTokenSource();
            StartFoldAnimation(_animationCancellationToken.Token, FoldTime, false).Forget();
        }

        private async UniTaskVoid StartFoldAnimation(CancellationToken token, float time, bool isUnfoldAnimation)
        {
            float foldTime = time;
            float baseValue = isUnfoldAnimation ? 0 : 1f;
            float mult = isUnfoldAnimation ? -1f : 1f;
            
            while (foldTime > 0f)
            {
                foldTime -= Time.deltaTime;
                float evaluateTime = baseValue + mult * foldTime / time;
                
                foreach (var tweenAnimation in Animations)
                {
                    tweenAnimation.Evaluate(evaluateTime);
                }
                await UniTask.Yield(cancellationToken: token);
            }
            _animatingProcessState = 0;
        }
    }
}
