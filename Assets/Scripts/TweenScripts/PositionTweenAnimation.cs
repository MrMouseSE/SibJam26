using UnityEngine;

namespace TweenScripts
{
    public class PositionTweenAnimation : TweenAnimation
    {
        public Transform TweenTransform;
        public Vector3 FromPosition;
        public Vector3 ToPosition;
        public bool LocalAnimate;

        public override void SetForceState(bool isForceStart)
        {
            SetPosition(isForceStart ? FromPosition : ToPosition);
        }

        public override void Evaluate(float value)
        {
            SetPosition(Vector3.LerpUnclamped(FromPosition, ToPosition, GetRuleValue(value)));
        }

        private void SetPosition(Vector3 position)
        {
            if (LocalAnimate)
                TweenTransform.localPosition = position;
            else
                TweenTransform.position = position;
        }
    }
}