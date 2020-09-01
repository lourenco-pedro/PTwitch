using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace TwitchChat_frntEnd
{
    public static class ClientUtil
    {

        public static Client GetClient()
        {
            Manager manager = SingletonUtil.GetMain();
            return manager.Client;
        }

        public static Client LoadClient()
        {
            string jsonPath = AccessUtil.GetCredentialsJsonPath();
            string filePath = string.Empty;

#if UNITY_EDITOR
            filePath = jsonPath + "/DevClient.json";
#else
            filePath = jsonPath + "/client.json";
#endif
            try
            {
                string clientJson = File.ReadAllText(filePath);
                Client client = JsonUtility.FromJson<Client>(clientJson);

                return client;
            }
            catch
            {
                return null;
            }
        }

        public static bool LoadClient(out Client client)
        {
            client = LoadClient();
            return client != null;
        }

        public static void SetClient()
        {
            Client client = GetClient();
            string clientJson = JsonUtility.ToJson(client);

            string jsonPath;

#if UNITY_EDITOR
            jsonPath = AccessUtil.GetCredentialsJsonPath() + "/DevClient.json";
#else
            jsonPath = AccessUtil.GetCredentialsJsonPath() + "/client.json";
#endif

            File.WriteAllText(jsonPath, clientJson);
        }

        public static bool IsConnected()
        {
            Client client = LoadClient();
            return client.Connected;
        }
    }
}