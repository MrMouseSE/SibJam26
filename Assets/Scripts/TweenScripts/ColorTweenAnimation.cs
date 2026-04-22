using UnityEngine;
using UnityEngine.UI;

namespace TweenScripts
{
    public class ColorTweenAnimation : TweenAnimation
    {
        public Image Image;
        public Color FromColor;
        public Color ToColor;

        public override void SetForceState(bool isForceStart)
        {
            Color color = isForceStart ? FromColor : ToColor;
            Image.color = color;
        }

        public override void Evaluate(float value)
        {
            Image.color = Color.Lerp(FromColor, ToColor, GetRuleValue(value));
        }
    }
}