using System.Threading;
using Cysharp.Threading.Tasks;
using TweenScripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MainMenuScripts.SettingsHanlderScripts
{
    public class SettingsHandlerContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public SettingsHandlerAnimationDescription AnimationDescription;
        
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
            StartFoldAnimation(_animationCancellationToken.Token, true).Forget();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(_animatingProcessState == -1) return;
            _animatingProcessState = -1;
            _animationCancellationToken.Cancel();
            _animationCancellationToken = new CancellationTokenSource();
            StartFoldAnimation(_animationCancellationToken.Token, false).Forget();
        }

        private async UniTaskVoid StartFoldAnimation(CancellationToken token, bool isUnfoldAnimation)
        {
            float foldTime;
            AnimationCurve ruleCurve;
            if (isUnfoldAnimation)
            {
                foldTime = AnimationDescription.UnfoldTime;
                ruleCurve = AnimationDescription.UnfoldRuleCurve;
            }
            else
            {
                foldTime = AnimationDescription.FoldTime;
                ruleCurve = AnimationDescription.FoldRuleCurve;
            }
            
            while (foldTime > 0f)
            {
                foldTime -= Time.deltaTime;
                float evaluateTime;
                if (isUnfoldAnimation)
                {
                    evaluateTime = 1 - foldTime / AnimationDescription.FoldTime;
                }
                else
                {
                    evaluateTime = foldTime / AnimationDescription.FoldTime;
                }
                float evaluateVal = ruleCurve.Evaluate(evaluateTime);
                foreach (var tweenAnimation in Animations)
                {
                    tweenAnimation.Evaluate(evaluateVal);
                }
                await UniTask.Yield(cancellationToken: token);
            }
            _animatingProcessState = 0;
        }
    }
}
