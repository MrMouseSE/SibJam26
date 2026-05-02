using UnityEditor;

namespace TweenScripts.Editor
{
    [CustomEditor(typeof(TweenGroupAnimation))]
    public class TweenGroupAnimationEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var nameProperty = serializedObject.FindProperty("TweenGroupName");
            EditorGUILayout.PropertyField(nameProperty);
            var arrayProperty = serializedObject.FindProperty("Animations");
            EditorGUILayout.PropertyField(arrayProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
