using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class UIUtil
    {
        public static void InitializeUI()
        {
            Manager manager = SingletonUtil.GetMain();

            manager.UI = GameObject.Instantiate(manager.Prefabs.UIHolder);
            manager.UI.transform.position = Vector3.zero;
            GameObject.DontDestroyOnLoad(manager.UI);
        }

        public static void UpdateUI()
        {
            State state = SimulationUtil.GetCurrentState();

            if (state == State.INTIALIZE)
                return;

            UIPanelUtil.ManagePanels();
        }

        public static UIHolder GetUIHolder()
        {
            return SingletonUtil.GetMain().UI;
        }

        public static void FormatUIText(UIText uiText, Character character)
        {
            if (uiText.text.Contains(Constants.TEXT_KEY_USERNAME))
                uiText.text = uiText.text.Replace(Constants.TEXT_KEY_USERNAME, character.FollowerReference.UserName);
        }
    }
}