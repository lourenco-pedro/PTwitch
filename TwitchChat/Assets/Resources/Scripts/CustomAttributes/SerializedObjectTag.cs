using UnityEngine;

namespace TwitchChat_frntEnd
{
    public class SerializedObjectTag : PropertyAttribute
    {
        public Color color;

        public SerializedObjectTag(string color = "#737373")
        {
            this.color = Color.white;
            ColorUtility.TryParseHtmlString(color, out this.color);
        }
    }
}