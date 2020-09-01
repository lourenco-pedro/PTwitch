using UnityEngine;

namespace TwitchChat_frntEnd
{
    public class Separator : PropertyAttribute
    {
        public Vector2 direction;

        public Separator(bool onTop = true)
        {
            this.direction = -Vector2.up;

            if (!onTop)
                this.direction = Vector2.up;
        }
    }
}