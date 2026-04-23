using UnityEngine;

namespace MainMenuScripts.SettingsHanlderScripts
{
    [CreateAssetMenu(menuName = "Create SettingsHandlerAnimationDescription", fileName = "SettingsHandlerAnimationDescription", order = 0)]
    public class SettingsHandlerAnimationDescription : ScriptableObject
    {
        public float FoldTime;
        public float UnfoldTime;
    }
}