using UnityEngine;

namespace TwitchChat_frntEnd
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomPropertyDrawer(typeof(Separator))]
    public class SeparatorEditor : PropertyDrawer
    {

        // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        // {
        //     return EditorGUI.GetPropertyHeight(property, label, true);
        // }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // EditorGUI.PropertyField(position, property, label, true);
            Separator separator = attribute as Separator;
            Vector2 direction = separator.direction;
            Rect linePosition = position;

            linePosition.position += direction * (GetPropertyHeight(property, label) / 2) * 1.5f;

            EditorGUI.LabelField(linePosition, "", GUI.skin.horizontalSlider);
        }
    }
#endif
}