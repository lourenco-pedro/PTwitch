using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchChat_bckEnd
{
    [System.Serializable]
    public class FollowerMessageInfo
    {
        public string Message;
        public string Time;
        
        public FollowerMessageInfo(string message, string time) 
        {
            this.Message = message;
            this.Time = time;
        }
    }
}
