using TMPro;
using UnityEngine;

namespace TweenScripts
{
    public class CountTweenAnimation : TweenAnimation
    {
        public TMP_Text Text;
        public float FromCount;
        public float ToCount;

        public override void SetForceState(bool isForceStart)
        {
            float count = isForceStart ? FromCount : ToCount;
            Text.text = count.ToString("0");
        }

        public override void Evaluate(float value)
        {
            Text.text = Mathf.Lerp(FromCount, ToCount, GetRuleValue(value)).ToString("0");
        }
    
    }
}
