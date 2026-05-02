using UnityEngine;

namespace AnimationDescriptionsScripts
{
    [CreateAssetMenu(menuName = "Animations/CameraAnimationDescription", fileName = "CameraAnimationDescription", order = 0)]
    public class CameraAnimationDescription : ScriptableObject
    {
        public float CameraAnimationDuration;
        public AnimationCurve CameraAnimationCurve;
    }
}