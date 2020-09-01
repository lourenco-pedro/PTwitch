using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class SingletonUtil
    {
        public static void InitializeSingleton(Manager manager)
        {
            Manager.main = manager;
        }

        public static Manager GetMain()
        {
            return Manager.main;
        }
    }
}