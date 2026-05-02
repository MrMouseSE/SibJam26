using UnityEngine;

namespace AnimationDescriptionsScripts
{
    [CreateAssetMenu(menuName = "Animations/BoilDescription", fileName = "BoilDescription", order = 0)]
    public class BoilDescription : ScriptableObject
    {
        public float BoilAnimationDuration;
        public float BoilExtreemeValue;
        public Vector3 ArrowExtreemeAngle;
        public Color NormalColor;
        public Color ExtreemeColor;
        
        public Vector2 BoilAddRangeMultiplier;
        public AnimationCurve BoilMultiplyerCurve;
        
        public float BoilProcessAnimationDuration;
    }
}