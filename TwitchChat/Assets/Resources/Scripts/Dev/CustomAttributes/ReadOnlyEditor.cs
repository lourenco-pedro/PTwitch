using UnityEngine;

namespace TwitchChat_frntEnd
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomPropertyDrawer(typeof(ReadOnly))]
    public class ReadOnlyEditor : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // GUI.enabled = false;
            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
            EditorGUI.EndDisabledGroup();
            // GUI.enabled = true;
        }
    }
#endif
}