using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TwitchChat_frntEnd
{
    public class LoginSettingsPanelComponent : UIPanel
    {
        public TMP_InputField InputField_JsonPath;
#if UNITY_EDITOR
        public TMP_InputField InputField_DevJsonPath;
#endif
    }
}
