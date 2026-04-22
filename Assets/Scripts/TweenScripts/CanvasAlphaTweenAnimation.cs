using UnityEngine;

namespace TweenScripts
{
    public class CanvasAlphaTweenAnimation : TweenAnimation
    {
        public CanvasGroup CanvasGroup;
        public float AlphaFrom;
        public float AlphaTo;

        public override void SetForceState(bool isForceStart)
        {
            float alphaValue = isForceStart ? AlphaFrom : AlphaTo;
            CanvasGroup.alpha = alphaValue;
        }

        public override void Evaluate(float value)
        {
            CanvasGroup.alpha = Mathf.Lerp(AlphaFrom, AlphaTo, GetRuleValue(value));
        }
    }
}