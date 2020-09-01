using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TwitchChat_frntEnd
{
    public class LoginPanelComponent : UIPanel
    {
        public TMP_InputField InputField_Username;
        public TMP_InputField InputField_Token;
        public Button Button_Submit;
    }
}