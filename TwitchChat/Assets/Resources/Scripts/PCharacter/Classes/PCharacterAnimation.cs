using UnityEngine;
using System;

namespace PCharacter
{
    [Serializable]
    public class PCharacterAnimation
    {
        public string Name;
        public PAnimationType AnimationType;
        public PFrame[] PFrames;

#if UNITY_EDITOR
        [HideInInspector] public int LastFrameCount;
        public bool ShowPFrames { get; set; }
#endif
    }
}