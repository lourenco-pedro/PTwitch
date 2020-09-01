using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PCharacter
{
    [System.Serializable]
    public class PFrame
    {
        public string FrameName;
#if UNITY_EDITOR
        public int SelectedIndex;
#endif
    }
}