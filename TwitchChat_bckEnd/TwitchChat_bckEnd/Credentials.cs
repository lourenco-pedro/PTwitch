using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace TwitchChat_bckEnd
{   public class Credentials
    {

        public string UserName;
        public string AccessToken;
        public bool Connected;
        public List<Follower> Followers;

        public static Credentials LoadCredentials() 
        {
        
            string clientJson = File.ReadAllText(Program.CredentialsJsonPath);
            Credentials deserializedClient = JsonConvert.DeserializeObject<Credentials>(clientJson);
            Console.WriteLine($"Credentials Loaded: {deserializedClient.UserName}");
            return deserializedClient;
        }

        public static void SetCredentials(Credentials credentials) 
        {
            string clientJson = JsonConvert.SerializeObject(credentials);
            File.WriteAllText(Program.CredentialsJsonPath, clientJson);
        }
    }
}
