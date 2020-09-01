using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
#if UNITY_EDITOR

    using UnityEditor;

    [CustomEditor(typeof(CharacterConfiguration))]
    public class CharacterConfigurationCustomInspector : Editor
    {

        public override void OnInspectorGUI()
        {
            CharacterConfiguration characterConfiguration = target as CharacterConfiguration;

            if (characterConfiguration == null)
                return;

            SerializedObject serialized_characterConfiguration = new SerializedObject(characterConfiguration);

            SerializedProperty baseNameProperty = serialized_characterConfiguration.FindProperty("BaseName");
            SerializedProperty moveSpeedProperty = serialized_characterConfiguration.FindProperty("MoveSpeed");
            SerializedProperty idleTimeRangeProperty = serialized_characterConfiguration.FindProperty("IdleTime");
            SerializedProperty usernameOffsetProperty = serialized_characterConfiguration.FindProperty("UsernameOffset");
            SerializedProperty usernameSizeProperty = serialized_characterConfiguration.FindProperty("UsernameSize");
            SerializedProperty usernameColorProperty = serialized_characterConfiguration.FindProperty("UsernameColor");

            DrawSectionHeader("Appearance");

            EditorGUILayout.PropertyField(baseNameProperty);

            EditorGUILayout.Space(10f);

            EditorGUILayout.PropertyField(usernameOffsetProperty);
            EditorGUILayout.PropertyField(usernameSizeProperty);
            EditorGUILayout.PropertyField(usernameColorProperty);

            EditorGUILayout.Space(10f);
            DrawSectionHeader("Mechanics");

            EditorGUILayout.PropertyField(moveSpeedProperty);


            EditorGUILayout.Space(10f);

            characterConfiguration.RandomIdleTime = EditorGUILayout.Toggle("Random Idle Time", characterConfiguration.RandomIdleTime);
            if (characterConfiguration.RandomIdleTime)
            {
                EditorGUILayout.PropertyField(idleTimeRangeProperty);
            }
            else
            {
                characterConfiguration.IdleTime.x = EditorGUILayout.FloatField("Idle Time", characterConfiguration.IdleTime.x);
                characterConfiguration.IdleTime.y = characterConfiguration.IdleTime.x;
            }


            serialized_characterConfiguration.ApplyModifiedProperties();
            EditorUtility.SetDirty(characterConfiguration);
        }

        private static void DrawSectionHeader(string title)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 18;
            style.fontStyle = FontStyle.Bold;

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label(title, style);
        }
    }
#endif
}