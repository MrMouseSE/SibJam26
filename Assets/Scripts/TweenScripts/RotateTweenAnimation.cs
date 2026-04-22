using UnityEngine;

namespace TweenScripts
{
    public class RotateTweenAnimation : TweenAnimation
    {
        public Transform TweenTransform;
        public Vector3 FromRotation;
        public Vector3 ToRotation;
        public bool LocalAnimate;

        public override void SetForceState(bool isForceStart)
        {
            SetRotation(isForceStart ? FromRotation : ToRotation);
        }

        public override void Evaluate(float value)
        {
            SetRotation(Vector3.LerpUnclamped(FromRotation, ToRotation, GetRuleValue(value)));
        }

        private void SetRotation(Vector3 rotationValues)
        {
            Quaternion rotation = Quaternion.Euler(rotationValues);
            if (LocalAnimate)
                TweenTransform.localRotation = rotation;
            else
                TweenTransform.rotation = rotation;
        }
    }
}