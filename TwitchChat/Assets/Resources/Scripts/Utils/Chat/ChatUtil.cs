using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PCharacter;

namespace TwitchChat_frntEnd
{
    public static class ChatUtil
    {
        public static void UpdateChat()
        {
            UpdateChatGround();
            UpdateCharacters();
        }

        public static void InitializeChat()
        {
            Manager manager = SingletonUtil.GetMain();
            Chat chat = GetChat();

            chat.Ground = GameObject.Instantiate(manager.Prefabs.Ground);
            SetObjectLayer(chat.Ground.gameObject, LayerMask.NameToLayer(chat.GroundLayer), true);
        }

        public static Chat GetChat()
        {
            Manager manager = SingletonUtil.GetMain();
            return manager.Chat;
        }

        private static void UpdateChatGround()
        {
            Chat chat = GetChat();
            MainCameraComponent mainCameraComponent = CameraUtil.GetMainCameraComponent();

            Vector2 middleWorldPos = Vector2.zero;
            Vector2 leftWorldPos = Vector2.zero;
            Vector2 rightWorldPos = Vector2.zero;

            CameraUtil.GetScreenToWorldPointBoundaires(ref leftWorldPos, ref middleWorldPos, ref rightWorldPos);

            float distanceBtweenScreenBounds = Vector2.Distance(leftWorldPos, rightWorldPos);

            middleWorldPos += Vector2.up * chat.GroundYOfset;
            chat.Ground.transform.position = middleWorldPos;
            chat.Ground.transform.localScale = Vector2.one + (Vector2.right * distanceBtweenScreenBounds);
        }

        private static void UpdateCharacters()
        {
            Client client = ClientUtil.GetClient();
            Chat chat = GetChat();

            for (int i = 0; i < client.Followers.Count; i++)
            {
                var curFollower = client.Followers[i];
                Character character = null;

                if (ExistInChat(out character, curFollower))
                {
                    //CHECK FOR ANY NEW MESSAGES HERE

                    character.PCharacter.gameObject.SetActive(GetCharacterActiveState(character));

                    UpdateCharacterMechanicsValues(character);
                    UpdateCharacterPosition(character);
                    UpdateCharacterStates(character);
                    UpdateCharacterAnimation(character);
                    continue;
                }

                chat.Characters.Add(AddCharacter(curFollower));
            }
        }

        private static bool ExistInChat(out Character character, Follower follower)
        {
            Chat chat = GetChat();

            character = null;

            for (int i = 0; i < chat.Characters.Count; i++)
            {
                var curCharacter = chat.Characters[i];
                if (curCharacter.FollowerReference == follower)
                {
                    character = curCharacter;
                    return true;
                }
            }

            return false;
        }

        private static Character AddCharacter(Follower follower)
        {
            Manager manager = SingletonUtil.GetMain();
            Character character = new Character();

            character.PCharacter = PCharacterInstanceUtil.CreateInstanceFromBase(CharacterCollectionUtil.GetDefaultCharacter());
            character.Position = Vector2.zero + Vector2.up;
            character.FollowerReference = follower;
            SetObjectLayer(character.PCharacter.gameObject, LayerMask.NameToLayer(manager.Chat.CharacterLayer), true);

            character.PCharacter.gameObject.SetActive(false);

            return character;
        }

        private static void SetObjectLayer(GameObject gameObject, int layer, bool changeChild)
        {
            gameObject.layer = layer;

            if (changeChild)
            {
                foreach (Transform child in gameObject.transform)
                {
                    SetObjectLayer(child.gameObject, layer, true);
                }
            }
        }

        private static void UpdateCharacterMechanicsValues(Character character)
        {
            Chat chat = GetChat();
            CharacterConfiguration characterConfiguration = chat.CharacterConfiguration;

            character.PCharacter.name = characterConfiguration.BaseName;

            if (character.PCharacter.name.Contains(Constants.TEXT_KEY_USERNAME))
            {
                character.PCharacter.name = character.PCharacter.name.Replace(Constants.TEXT_KEY_USERNAME, character.FollowerReference.UserName);
            }

            character.MoveSpeed = characterConfiguration.MoveSpeed;
        }

        private static void UpdateCharacterStates(Character character)
        {

            Chat chat = GetChat();

            Vector2 worldBottomLeft = Vector2.zero;
            Vector2 worldBottomCenter = Vector2.zero;
            Vector2 worldBottomRight = Vector2.zero;

            switch (character.State)
            {
                case CharacterState.IDLE:
                    {
                        character.IdleCurrentTime += Time.deltaTime / character.IdleTime;
                        if (character.IdleCurrentTime >= 1)
                        {
                            character.IdleCurrentTime = 0;

                            CameraUtil.GetScreenToWorldPointBoundaires(ref worldBottomLeft, ref worldBottomCenter, ref worldBottomRight);

                            character.DestinationXPosition = Random.Range(worldBottomLeft.x, worldBottomRight.x);

                            character.State = CharacterState.WALK;
                        }
                    }
                    break;
                case CharacterState.WALK:
                    {

                        int direction;

                        if (character.DestinationXPosition > character.Position.x)
                        {
                            character.PCharacter.Controller.CurrentAction = PControllerActionType.TRANSLATE_RIGHT;
                            direction = 1;
                        }
                        else
                        {
                            character.PCharacter.Controller.CurrentAction = PControllerActionType.TRANSLATE_LEFT;
                            direction = -1;
                        }

                        PCharacter.PCharacterInstanceUtil.FaceToMoveDirection(character.PCharacter);

                        Vector2 newPosition = character.Position;

                        newPosition.x += direction * character.MoveSpeed * Time.deltaTime;
                        character.Position = newPosition;

                        if (Mathf.Abs(character.Position.x - character.DestinationXPosition) < .1f)
                        {
                            character.State = CharacterState.IDLE;
                            character.IdleTime = Random.Range(chat.CharacterConfiguration.IdleTime.x, chat.CharacterConfiguration.IdleTime.y);
                        }
                    }
                    break;
            }
        }

        private static void UpdateCharacterAnimation(Character character)
        {
            switch (character.State)
            {
                case CharacterState.IDLE:
                    PCharacterInstanceUtil.SetInstanceAnimation(character.PCharacter, "Idle");
                    break;
                case CharacterState.WALK:
                    PCharacterInstanceUtil.SetInstanceAnimation(character.PCharacter, "Walk");
                    break;
            }

            PCharacterInstanceUtil.UpdateInstanceAnimation(character.PCharacter);
        }

        private static void UpdateCharacterPosition(Character character)
        {
            Chat chat = GetChat();
            Vector2 curPosition = character.Position;

            //GET GROUND Y SURFACE - CHARACTER WILL ALWAYS STAY RIGHT UP ON GROUND SURFACE
            int groundLayer = LayerMask.NameToLayer(chat.GroundLayer);

            RaycastHit2D groundHit = Physics2D.Raycast(Vector2.zero, -Vector2.up * 1000);
            if (groundHit && groundHit.transform.gameObject.layer == groundLayer)
                curPosition.y = groundHit.point.y;

            character.Position = curPosition;
        }

        private static bool GetCharacterActiveState(Character character)
        {

            //CHARACTER WILL ONLY SHOW UP IF HE IS NOT IN FOLLOWERS TO ADD QUEUE AND IF HE IS NOT BEN SHWING AT
            //THE NEW FOLLOWER OBEJCT 

            Client client = ClientUtil.GetClient();
            Simulation simulation = SimulationUtil.GetSimulation();
            Chat chat = GetChat();

            if (character.FollowerReference.UserName == simulation.NewFollower.UserName)
                return false;

            foreach (Follower follower in simulation.FollowersToAdd)
            {
                if (follower.UserName == character.FollowerReference.UserName)
                    return false;
            }

            return true;
        }
    }
}
