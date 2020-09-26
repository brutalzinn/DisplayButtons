using DisplayButtons.Bibliotecas.OAuthConsumer;
using DisplayButtons.Bibliotecas.OAuthConsumer.Auths;
using DisplayButtons.Bibliotecas.OAuthConsumer.TwitchEvents;
using MyService;
using Newtonsoft.Json;

using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users;

using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using static DisplayButtons.Bibliotecas.OAuthConsumer.Services;

namespace DisplayButtons.Bibliotecas.TwitchWrapper
{

    public static class TwitchWrapper
    {
 
        private static TwitchClient client;
        private static string my_token;
        private static TwitchAPI api;
        private const string CredentialsPath = "credentials_twitch.json";
        private static readonly string? clientId = "h8ocjqn8n6fvv4jrr6oyzb0f923v7e";

     

        public static void StartTwitchApi()
        {
            TwitchAuth myTwitchApi = new TwitchAuth();

            myTwitchApi.Authenticator();
   
            // setApi();

            // Other code


        }
    
       private static void setApi()
        {

            api = new TwitchAPI();
            api.Settings.ClientId = clientId;
            api.Settings.AccessToken = my_token; // App Secret is not an Accesstoken
            foreach (User item in api.Helix.Users.GetUsersAsync().Result.Users)
            {

                Debug.WriteLine(item.DisplayName);
            }
        }
        private static void StartChat()
        {
            ConnectionCredentials credentials = new ConnectionCredentials("robertocpaes", my_token);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials);

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }
            private  static void Client_OnLog(object sender, OnLogArgs e)
            {
                Debug.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
            }

            private static void Client_OnConnected(object sender, OnConnectedArgs e)
            {
            Debug.WriteLine($"Connected to {e.AutoJoinChannel}");
            }

            private static void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
            {
            Debug.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
                client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
            }

            private static void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
            {
                if (e.ChatMessage.Message.Contains("badword"))
                    client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
            }

        private static  void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
            {
                if (e.WhisperMessage.Username == "my_friend")
                    client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
            }

            private static void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
            {
                if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
                    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
                else
                    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
            }
        }
    
}
