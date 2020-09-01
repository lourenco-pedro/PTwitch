using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class GameDebug
    {
        public static bool DebugUsers = true;

        #region GUI

        public static void DebugGUI()
        {

            State state = SimulationUtil.GetCurrentState();

            switch (state)
            {
                case State.IN_CHAT:
                    if (DebugUsers)
                    {
                        Rect debugUsersRect = new Rect(0, 0, 300, 100);
                        GUI.Window(Constants.GUI_DEBUG_USERS_ID, debugUsersRect, GUI_DrawDebugUsers, "Users");
                    }
                    break;
            }
        }

        private static string addUser_UserName;

        private static void GUI_DrawDebugUsers(int id)
        {
            GUILayout.Label("Username:");
            addUser_UserName = GUILayout.TextField(addUser_UserName);

            if (GUILayout.Button("Add"))
            {
                var follower = FollowerUtil.CreateFollower(addUser_UserName, new FollowerMessageInfo());
                SingletonUtil.GetMain().Client.Followers.Add(follower);
                SimulationUtil.GetSimulation().FollowersToAdd.Enqueue(follower);
            }
        }

        #endregion
    }
}