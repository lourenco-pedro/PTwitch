using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolSystem;

namespace TwitchChat_frntEnd
{
    public static class InChatUIUtil
    {
        public static void SetupPanel()
        {
        }

        public static void UpdatePanel()
        {
            DisplayCharactersUsername();
            UpdateUsernamesPosition();
            UpdateUsernamesAppearance();
        }

        private static void DisplayCharactersUsername()
        {
            Chat chat = ChatUtil.GetChat();
            Prefabs prefabs = SingletonUtil.GetMain().Prefabs;
            InChatPanelComponent inChatPanelComponent = (InChatPanelComponent)UIPanelUtil.GetUIPanel(PanelType.IN_CHAT);

            if (inChatPanelComponent.CharactersUsername.Length != chat.Characters.Count)
            {
                if (inChatPanelComponent.CharactersUsername.Length > chat.Characters.Count)
                {
                    int diff = inChatPanelComponent.CharactersUsername.Length - chat.Characters.Count;
                    for (int i = diff + 1; i < inChatPanelComponent.CharactersUsername.Length; i++)
                    {
                        inChatPanelComponent.CharactersUsername[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    int diff = chat.Characters.Count - inChatPanelComponent.CharactersUsername.Length;
                    for (int i = 0; i < diff; i++)
                    {
                        Pool.TryGetMonoBehaviourFromPool<UIText>(ref inChatPanelComponent.CharactersUsername, prefabs.UiText);
                    }
                }
            }
        }

        private static void UpdateUsernamesPosition()
        {
            Chat chat = ChatUtil.GetChat();
            MainCameraComponent cameraComponent = CameraUtil.GetMainCameraComponent();
            InChatPanelComponent inChatPanelComponent = (InChatPanelComponent)UIPanelUtil.GetUIPanel(PanelType.IN_CHAT);

            for (int i = 0; i < inChatPanelComponent.CharactersUsername.Length; i++)
            {
                UIText uiText = inChatPanelComponent.CharactersUsername[i];
                Character referenceCharacter = chat.Characters[i];

                uiText.transform.SetParent(inChatPanelComponent.CharacterUsernamesHolder);

                Vector2 worldPosition = chat.Characters[i].Position + Vector2.up * referenceCharacter.PCharacter.Render.sprite.bounds.size;
                Vector2 offset = chat.CharacterConfiguration.UsernameOffset;

                uiText.transform.position = cameraComponent.Camera.WorldToScreenPoint(worldPosition + offset);
            }
        }

        private static void UpdateUsernamesAppearance()
        {
            Chat chat = ChatUtil.GetChat();
            MainCameraComponent cameraComponent = CameraUtil.GetMainCameraComponent();
            InChatPanelComponent inChatPanelComponent = (InChatPanelComponent)UIPanelUtil.GetUIPanel(PanelType.IN_CHAT);

            for (int i = 0; i < inChatPanelComponent.CharactersUsername.Length; i++)
            {
                UIText uiText = inChatPanelComponent.CharactersUsername[i];
                Character referenceCharacter = chat.Characters[i];

                uiText.text = chat.CharacterConfiguration.BaseName;
                UIUtil.FormatUIText(uiText, referenceCharacter);

                uiText.fontSize = chat.CharacterConfiguration.UsernameSize;
                uiText.color = chat.CharacterConfiguration.UsernameColor;
            }
        }
    }
}