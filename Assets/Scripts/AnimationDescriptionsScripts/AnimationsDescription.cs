using UnityEngine;

namespace AnimationDescriptionsScripts
{
    [CreateAssetMenu(menuName = "Create AnimationsDescription", fileName = "AnimationsDescription", order = 0)]
    public class AnimationsDescription : ScriptableObject
    {
        public ElementsAnimationDescription ElementsAnimationDescription;
    }
}