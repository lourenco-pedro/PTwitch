using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;

namespace TwitchChat_frntEnd
{
    public class ButtonGeneralNavigation : MonoBehaviour
    {
        public void SetState(string state)
        {
            State toState;
            if (Enum.TryParse(state, out toState))
            {
                SimulationUtil.SetState(toState);
            }
        }

        public void PushState(string state)
        {
            State toState;
            if (Enum.TryParse(state, out toState))
            {
                SimulationUtil.AddState(toState);
            }
        }

        public void PopState()
        {
            SimulationUtil.PopState();
        }

        public void Login()
        {
            LoginPanelComponent loginPanelComponent = (LoginPanelComponent)UIPanelUtil.GetUIPanel(PanelType.LOGIN);

            Client client = ClientUtil.GetClient();
            client.UserName = loginPanelComponent.InputField_Username.text;
            client.AccessToken = loginPanelComponent.InputField_Token.text;
            client.ReadMessages = false;
            client.Followers = new List<Follower>();

            ClientUtil.SetClient();

            Simulation simulation = SimulationUtil.GetSimulation();
            ProcessStartInfo backEnd = new ProcessStartInfo();
            backEnd.FileName = "TwitchChat_bckEnd.exe";
            backEnd.WorkingDirectory = AccessUtil.GetBackEndProcessPath();
            simulation.Backend = Process.Start(backEnd);
        }

        public void SaveLoginSettings()
        {
            Manager manager = SingletonUtil.GetMain();
            Access access = AccessUtil.GetAccessData();
            LoginSettingsPanelComponent loginSettingsPanelComponent = (LoginSettingsPanelComponent)UIPanelUtil.GetUIPanel(PanelType.LOGIN_SETTINGS);

#if UNITY_EDITOR
            access.DevJsonPath = loginSettingsPanelComponent.InputField_DevJsonPath.text;
#endif
            access.JsonPath = loginSettingsPanelComponent.InputField_JsonPath.text;

#if UNITY_EDITOR
            PlayerPrefs.SetString("DevJsonPath", access.DevJsonPath);
#endif
            PlayerPrefs.SetString("JsonPath", access.JsonPath);
#if UNITY_EDITOR
            if (loginSettingsPanelComponent.InputField_DevJsonPath.text != string.Empty)
            {
                ClientUtil.SetClient();
            }
#else
            if (loginSettingsPanelComponent.InputField_JsonPath.text != string.Empty)
            {
                ClientUtil.SetClient();
            }
#endif
        }
    }
}
