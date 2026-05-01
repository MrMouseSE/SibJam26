using UnityEngine;

namespace AnimationDescriptionsScripts
{
    [CreateAssetMenu(menuName = "Create ElementsAnimationDescription", fileName = "ElementsAnimationDescription", order = 0)]
    public class ElementsAnimationDescription : ScriptableObject
    {
        public float ElementDrawAnimationDuration;
        public float ElementSelectionAnimationDuration;
        public float ElementHoverAnimationDuration;
    }
}