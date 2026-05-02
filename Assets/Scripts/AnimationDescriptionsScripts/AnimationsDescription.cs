using TweenScripts;
using UnityEngine;

namespace AnimationDescriptionsScripts
{
    [CreateAssetMenu(menuName = "Animations/AnimationsDescription", fileName = "AnimationsDescription", order = 0)]
    public class AnimationsDescription : ScriptableObject
    {
        public ElementsAnimationDescription ElementsAnimationDescription;
        
        public float ButtonAnimationDuration;
        public float ButtonHoverAnimationDuration;
        public float ClickAnimationsDuration;
    }
}