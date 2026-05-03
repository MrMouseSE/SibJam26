using TMPro;
using UnityEngine;

namespace TweenScripts
{
    public class ColorTextTweenAnimation : TweenAnimation
    {
        public TMP_Text Text;
        public Color FromColor = Color.white;
        public Color ToColor = Color.white;

        public override void SetForceState(bool isForceStart)
        {
            Color color = isForceStart ? FromColor : ToColor;
            Text.color = color;
        }

        public override void Evaluate(float value)
        {
            Text.color = Color.Lerp(FromColor, ToColor, GetRuleValue(value));
        }
    }
}
