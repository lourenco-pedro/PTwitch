using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PCharacter;

namespace TwitchChat_frntEnd
{
    [CreateAssetMenu(fileName = "Character Collection", menuName = "Chat/Character Collection")]
    public class CharacterBaseCollection : ScriptableObject
    {
        public string Name;

        [Space(10f)]
        public CharacterCollectionItem[] Characters;
    }

    [System.Serializable]
    public class CharacterCollectionItem
    {
        public string Name;
        public PCharacter.Character Base;
    }
}