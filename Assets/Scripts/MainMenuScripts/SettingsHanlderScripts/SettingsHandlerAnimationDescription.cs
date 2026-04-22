using UnityEngine;

namespace MainMenuScripts.SettingsHanlderScripts
{
    [CreateAssetMenu(menuName = "Create SettingsHandlerAnimationDescription", fileName = "SettingsHandlerAnimationDescription", order = 0)]
    public class SettingsHandlerAnimationDescription : ScriptableObject
    {
        public float FoldTime;
        public AnimationCurve FoldRuleCurve;
        
        public float UnfoldTime;
        public AnimationCurve UnfoldRuleCurve;
    }
}