using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Clients;

namespace TwitchChat_bckEnd
{
    public class Client
    {

        TwitchClient TClient;
        Credentials Credentials;

        public bool IsConnected { get => TClient.IsConnected; }
        
        public void Connect(Credentials credentials)
        {

            Credentials = credentials;
            string username = credentials.UserName;
            string accesToken = credentials.AccessToken;

            ConnectionCredentials connectionCredentials = new ConnectionCredentials(username, accesToken);

            TClient = new TwitchClient();
            TClient.Initialize(connectionCredentials, username);

            TClient.OnConnected += TwitchClient_OnConnected;
            TClient.OnMessageReceived += TwitchClient_OnMessageReceived;

            TClient.Connect();
        }

        public void Disconnect() 
        {
            TClient.Disconnect();
        }

        private void TwitchClient_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("Bot connected !");
            TClient.SendMessage(Credentials.UserName, "Bot logged in !");
        }

        private void TwitchClient_OnMessageReceived(object sender, OnMessageReceivedArgs e) 
        {
            Follower follower = Follower.TryGetFollower(ref Credentials.Followers, e.ChatMessage.Username);
            
            FollowerMessageInfo messageInfo = Follower.CreateMessageInfo(e.ChatMessage.Message);
            follower.Messages.Add(messageInfo);

            Credentials.SetCredentials(Credentials);

            Console.WriteLine($"Message received by {e.ChatMessage.Username}: {e.ChatMessage.Message}");
        }
    }
}
