using UnityEngine;

namespace TwitchChat_frntEnd
{
    [System.Serializable]
    public class FollowerMessageInfo
    {
        [ReadOnly]
        public string Message;
        [ReadOnly]
        public string Time;
    }
}