using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class LoginUIUtil
    {

        public static void SetupPanel()
        {
            LoginPanelComponent loginPanelComponent = (LoginPanelComponent)UIPanelUtil.GetUIPanel(PanelType.LOGIN);

            string credentialsPath = string.Empty;
#if UNITY_EDITOR
            credentialsPath = AccessUtil.GetCredentialsJsonPath() + "/DevClient.json";
#else
            credentialsPath = AccessUtil.GetCredentialsJsonPath() + "/client.json";
#endif
            if (System.IO.File.Exists(credentialsPath))
            {
                Manager manager = SingletonUtil.GetMain();
                manager.Client = ClientUtil.LoadClient();

                loginPanelComponent.InputField_Username.text = manager.Client.UserName;
                loginPanelComponent.InputField_Token.text = manager.Client.AccessToken;
            }
        }

        public static void UpdatePanel()
        {
            LoginPanelComponent loginPanelComponent = (LoginPanelComponent)UIPanelUtil.GetUIPanel(PanelType.LOGIN);
            Access access = AccessUtil.GetAccessData();

            bool displayLogin = true;
#if UNITY_EDITOR
            displayLogin = access.DevJsonPath != string.Empty;
#else
            displayLogin = access.JsonPath != string.Empty;
#endif

            loginPanelComponent.InputField_Username.enabled = displayLogin;
            loginPanelComponent.InputField_Token.enabled = displayLogin;
            loginPanelComponent.Button_Submit.enabled = displayLogin && loginPanelComponent.InputField_Token.text != string.Empty && loginPanelComponent.InputField_Username.text != string.Empty;

            if (!loginPanelComponent.Button_Submit.enabled)
            {
                loginPanelComponent.Button_Submit.image.color = loginPanelComponent.Button_Submit.colors.disabledColor;
            }
            else
            {
                loginPanelComponent.Button_Submit.image.color = Color.white;
            }
        }
    }
}