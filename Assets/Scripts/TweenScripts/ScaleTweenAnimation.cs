using UnityEngine;

namespace TweenScripts
{
    public class ScaleTweenAnimation : TweenAnimation
    {
        public Transform TweenTransform;
        public Vector3 FromScale;
        public Vector3 ToScale;
        
        public override void SetForceState(bool isForceStart)
        {
            Vector3 scale = isForceStart ? FromScale : ToScale;
            TweenTransform.localScale = scale;
        }

        public override void Evaluate(float value)
        {
            Vector3 scale = Vector3.LerpUnclamped(FromScale, ToScale, GetRuleValue(value));
            TweenTransform.localScale = scale;
        }
    }
}