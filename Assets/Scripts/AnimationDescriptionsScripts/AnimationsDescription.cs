using UnityEngine;
using UnityEngine.Serialization;

namespace AnimationDescriptionsScripts
{
    [CreateAssetMenu(menuName = "Animations/AnimationsDescription", fileName = "AnimationsDescription", order = 0)]
    public class AnimationsDescription : ScriptableObject
    {
        public ElementsAnimationDescription ElementsAnimationDescription;
        
        public BoilDescription Boil;

        public CameraAnimationDescription Camera;
        
        [Space]
        [Header("Button Settings")]
        [Space]
        public float ActivateAnimationDuration;
        public float HoverAnimationDuration;
        public float ClickAnimationsDuration;
        [FormerlySerializedAs("TooltipAnimationDuration")] public float ButtonTooltipAnimationDuration;
        
        [Space]
        [Header("Button Settings")]
        [Space]
        public float ScoreAnimationDuration;
    }
}