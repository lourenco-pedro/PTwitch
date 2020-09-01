using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TwitchLib.Client;

namespace TwitchChat_bckEnd
{
    class Program
    {

        public const string CredentialsJsonPath = "";

        static void Main(string[] args)
        {
            Credentials credentials = Credentials.LoadCredentials();
            
            Client client = new Client();
            
            client.Connect(credentials);

            credentials.Connected = client.IsConnected;
            Credentials.SetCredentials(credentials);
            
            while (client.IsConnected) 
            {

                string command = Console.ReadLine();

                if (command == "Disconnect") 
                {
                    client.Disconnect();
                    credentials.Connected = false;
                    credentials.Followers = new List<Follower>();
                    Credentials.SetCredentials(credentials);
                }
            }
        }
    }
}
