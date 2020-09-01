using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class LoginSettingsUIUtil
    {
        public static void SetupPanel()
        {
            Manager manager = SingletonUtil.GetMain();
            Access access = manager.Access;
            LoginSettingsPanelComponent panel = (LoginSettingsPanelComponent)UIPanelUtil.GetUIPanel(PanelType.LOGIN_SETTINGS);

#if UNITY_EDITOR
            panel.InputField_DevJsonPath.text = AccessUtil.GetCredentialsJsonPath();
#else
            panel.InputField_JsonPath.text = AccessUtil.GetCredentialsJsonPath();
#endif
        }
    }
}