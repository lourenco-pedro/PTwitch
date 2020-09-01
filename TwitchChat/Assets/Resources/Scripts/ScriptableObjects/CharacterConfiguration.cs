using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    [CreateAssetMenu(fileName = "CharacterConfiguration", menuName = "Chat/CharacterConfiguration")]
    public class CharacterConfiguration : ScriptableObject
    {
        public string BaseName;

        public Vector2 UsernameOffset;
        public float UsernameSize;
        public Color UsernameColor;

        [Space(10f)]
        public float MoveSpeed;
        public bool RandomIdleTime;
        public Vector2 IdleTime;

    }
}