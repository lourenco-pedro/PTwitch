using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PCharacter;

namespace TwitchChat_frntEnd
{
    [System.Serializable]
    public class Character
    {

        public Follower FollowerReference;
        public CharacterState State;

        [Space(10f)]
        [ReadOnly]
        public PCharacterInstance PCharacter;

        [Space(10f)]
        [ReadOnly]
        public Vector2 FinalPosition;
        public Vector2 Position
        {
            get => PCharacter.transform.position;
            set => PCharacter.transform.position = value;
        }

        [Space(5f)]
        [ReadOnly]
        public float MoveSpeed;
        [ReadOnly]
        public float IdleTime;
        [HideInInspector]
        public float IdleCurrentTime;
        [HideInInspector]
        public float DestinationXPosition;
    }
}
