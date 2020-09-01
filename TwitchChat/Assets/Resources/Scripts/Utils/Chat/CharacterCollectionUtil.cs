using UnityEngine;
using PCharacter;

namespace TwitchChat_frntEnd
{
    public static class CharacterCollectionUtil
    {

        public static CharacterBaseCollection GetCollection(string collectionName)
        {
            Manager manager = SingletonUtil.GetMain();

            for (int i = 0; i < manager.Prefabs.CharacterCollection.Length; i++)
            {
                CharacterBaseCollection collection = manager.Prefabs.CharacterCollection[i];
                if (collection.Name == collectionName)
                    return collection;
            }

            return null;
        }

        public static PCharacter.Character GetCharacterInCollection(CharacterBaseCollection collection, string characterName)
        {
            for (int i = 0; i < collection.Characters.Length; i++)
            {
                CharacterCollectionItem collectionItem = collection.Characters[i];
                if (collectionItem.Name == characterName)
                    return collectionItem.Base;
            }

            return null;
        }

        public static PCharacter.Character GetDefaultCharacter()
        {
            CharacterBaseCollection defaultCollection = GetCollection("DefaultCollection");
            PCharacter.Character defaultCharacter = GetCharacterInCollection(defaultCollection, "Mario");
            return defaultCharacter;
        }
    }
}