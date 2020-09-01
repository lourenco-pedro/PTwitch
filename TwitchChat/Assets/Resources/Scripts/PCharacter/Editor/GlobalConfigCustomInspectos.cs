#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace PCharacter
{
    [CustomEditor(typeof(PCharacterGlobalConfig))]
    public class GlobalConfigCustomInspectos : Editor
    {

        public override void OnInspectorGUI()
        {
            var globalConfig = (PCharacterGlobalConfig)target;
            DrawDefaultInspector();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Constraints ");
                globalConfig.Constraints2D = (RigidbodyConstraints2D)EditorGUILayout.EnumFlagsField(globalConfig.Constraints2D);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif