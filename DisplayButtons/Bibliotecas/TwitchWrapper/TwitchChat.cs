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

        public static bool Auth()
        {
            // This is a bug in the SWAN Logging library, need this hack to bring back the cursor


            if (string.IsNullOrEmpty(clientId))
            {
                throw new NullReferenceException(
                  "Please set SPOTIFY_CLIENT_ID via environment variables before starting the program"
                );
            }

            if (File.Exists(CredentialsPath))
            {
                return true;
            }
            else
            {
                StartAuthentication();
                return false;
            }



        }

        private static void StartAuthentication()
        {

            //if(oAuthTokenTwitch == null)
            //       {
            //       oAuthTokenTwitch =  new SimpleWebServer.SimpleHTTPServer(5000,ServicesEnum.Twitch);

            //       }

            WebService.StartWebServer(ServicesEnum.Twitch);

            string url = String.Format("https://id.twitch.tv/oauth2/authorize?client_id={0}&redirect_uri={1}&response_type={2}&scope={3}", clientId, "http://localhost:5000/callback", "token", "");

  
            Uri uri = (new Uri(url));
            try
            {
                BrowserUtil.Open(uri);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to open URL, manually open: {0}", uri);
            }

            Globals.events.On("TwitchEventHandlerLoginOauth", (e) => {
                // Cast event argrument to your event object
                var obj = (TwitchEventHandlerLoginOauth)e;

                // Get (set) your event object data
                my_token = obj.token;
                //    type = obj.type;

                //   WebService.StopWebServer();
                setApi();

                // Other code
            });

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
