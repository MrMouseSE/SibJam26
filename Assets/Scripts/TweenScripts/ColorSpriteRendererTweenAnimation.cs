using UnityEngine;

namespace TweenScripts
{
    public class ColorSpriteRendererTweenAnimation : TweenAnimation
    {
        public SpriteRenderer Renderer;
        public Color FromColor = Color.white;
        public Color ToColor = Color.white;

        public override void SetForceState(bool isForceStart)
        {
            Color color = isForceStart ? FromColor : ToColor;
            Renderer.color = color;
        }

        public override void Evaluate(float value)
        {
            Renderer.color = Color.Lerp(FromColor, ToColor, GetRuleValue(value));
        }
    }
}