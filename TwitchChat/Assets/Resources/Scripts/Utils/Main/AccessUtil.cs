using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class AccessUtil
    {
        public static Access GetAccessData()
        {
            Manager manager = SingletonUtil.GetMain();
            return manager.Access;
        }

        public static void LoadAccess()
        {
            Access access = GetAccessData();
            string devJsonPath = PlayerPrefs.GetString("DevJsonPath", string.Empty);
            string jsonPath = PlayerPrefs.GetString("JsonPath", string.Empty);
#if UNITY_EDITOR
            SetCrednetialsJsonPath(devJsonPath);
#else
            SetCrednetialsJsonPath(jsonPath);
#endif
        }

        public static void SetCrednetialsJsonPath(string path)
        {
            Access access = GetAccessData();
#if UNITY_EDITOR
            access.DevJsonPath = path;
#else
            access.JsonPath = path;
#endif
        }

        public static string GetCredentialsJsonPath()
        {
            Access access = GetAccessData();
#if UNITY_EDITOR
            return access.DevJsonPath;
#else
            return access.JsonPath;
#endif      
        }

        public static string GetBackEndProcessPath()
        {
            Access access = GetAccessData();
#if UNITY_EDITOR
            return access.DevBackEndProcessPath;
#else
            return access.BackEndProcessPath;
#endif      
        }
    }
}
