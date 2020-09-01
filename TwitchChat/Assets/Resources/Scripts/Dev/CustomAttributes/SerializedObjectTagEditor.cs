using UnityEngine;

namespace TwitchChat_frntEnd
{
#if UNITY_EDITOR

    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializedObjectTag))]
    public class SerializedObjectTagEditor : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var reference = (SerializedObjectTag)attribute;

            GUIStyle style = new GUIStyle();
            style.fontSize = 18;
            style.normal.textColor = reference.color;
            style.fontStyle = FontStyle.Bold;

            EditorGUI.LabelField(position, label.text, style);
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }
    }
#endif
}