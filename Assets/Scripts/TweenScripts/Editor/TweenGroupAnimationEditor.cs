using UnityEditor;

namespace TweenScripts.Editor
{
    [CustomEditor(typeof(TweenGroupAnimation))]
    public class TweenGroupAnimationEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var arrayProperty = serializedObject.FindProperty("Animations");
            EditorGUILayout.PropertyField(arrayProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
