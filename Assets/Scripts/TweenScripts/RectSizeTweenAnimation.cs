using UnityEngine;

namespace TweenScripts
{
    public class RectSizeTweenAnimation : TweenAnimation
    {
        public RectTransform RectTransform;
        public Vector2 FromSize;
        public Vector2 ToSize;

        public override void SetForceState(bool isForceStart)
        {
            Vector2 forceSize = isForceStart ? FromSize : ToSize;
            RectTransform.sizeDelta = forceSize;
        }

        public override void Evaluate(float value)
        {
            RectTransform.sizeDelta = Vector2.Lerp(FromSize, ToSize, GetRuleValue(value));
        }
    }
}