using UnityEngine;
using System.Collections.Generic;

namespace TwitchChat_frntEnd
{
    [System.Serializable]
    public class Follower
    {
        [ReadOnly]
        public string UserName;
        public List<FollowerMessageInfo> Messages;
    }
}