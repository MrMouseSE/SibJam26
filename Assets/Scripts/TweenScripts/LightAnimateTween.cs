using UnityEngine;

namespace TweenScripts
{
    public class LightAnimateTween : TweenAnimation
    {
        public Light TweenLight;
        public float IntensityFrom;
        public float IntensityTo;

        public override void SetForceState(bool isForceStart)
        {
            TweenLight.intensity = isForceStart ? IntensityFrom : IntensityTo;
        }

        public override void Evaluate(float value)
        {
            TweenLight.intensity = Mathf.LerpUnclamped(IntensityFrom, IntensityTo, GetRuleValue(value));
        }
    }
}