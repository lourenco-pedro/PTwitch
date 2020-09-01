using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Client.Models;

namespace TwitchChat_bckEnd
{
    public class Follower
    {
        public string UserName;
        public List<FollowerMessageInfo> Messages;

        public Follower(string name, string message = "") 
        {
            this.UserName = name;
            this.Messages = new List<FollowerMessageInfo>();

            if (!String.IsNullOrEmpty(message))
            {
                FollowerMessageInfo followerMessageInfo = CreateMessageInfo(message);
                Messages.Add(followerMessageInfo);
            }
        }

        public static FollowerMessageInfo CreateMessageInfo(string message) 
        {
            var date = DateTime.Now;
            var timeZone = TimeZoneInfo.Local;
            string time = string.Format("{0}:{1}:{2}, {3}", date.Hour, date.Minute, date.Second, timeZone.DisplayName);
            FollowerMessageInfo follwerMessageInfo = new FollowerMessageInfo(message, time);
            return follwerMessageInfo;
        }

        public static Follower TryGetFollower(ref List<Follower> collection, string username) 
        {
            Follower followerToReturn = null;
            for (int i = 0; i < collection.Count; i++) 
            {
                if (collection[i].UserName == username) 
                {
                    followerToReturn = collection[i];
                    break;
                }
            }

            if (followerToReturn == null)
            {
                followerToReturn = new Follower(username);
                collection.Add(followerToReturn);
            }

            return followerToReturn;
        }
    }
}
