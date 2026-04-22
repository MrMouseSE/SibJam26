using UnityEngine;

namespace TweenScripts
{
    public class RectPositionTweenAnimation : TweenAnimation
    {
        public RectTransform RectTransform;
        public Vector2 FromPosition;
        public Vector2 ToPosition;

        public override void SetForceState(bool isForceStart)
        {
            Vector2 forcePosition = isForceStart ? FromPosition : ToPosition;
            RectTransform.anchoredPosition = forcePosition;
        }

        public override void Evaluate(float value)
        {
            RectTransform.anchoredPosition = Vector2.LerpUnclamped(FromPosition, ToPosition, GetRuleValue(value));
        }
    }
}