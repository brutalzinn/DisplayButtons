
using DisplayButtons.Backend.Objects.Implementation.DeckActions.General;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Swan;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using static SpotifyAPI.Web.Scopes;


namespace DisplayButtons.Bibliotecas.SpotifyWrapper
{
    public class Spotify
    {
        private const string CredentialsPath = "credentials.json";
        private static readonly string? clientId = "d1362b6ae88a4254900b41bdc1489503";
        private static readonly EmbedIOAuthServer _server = new EmbedIOAuthServer(new Uri("http://localhost:5000/callback"), 5000);

        
        public static async Task<bool> Auth()
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
                await StartAuthentication();
                return false;
            }

           
        
        }

        private static async Task Start()
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");

            var playlists = await spotify.PaginateAll(await spotify.Playlists.CurrentUsers().ConfigureAwait(false));
            Debug.WriteLine($"Total Playlists in your Account: {playlists.Count}");

            _server.Dispose();
          //  Environment.Exit(0);
        }

        public static async Task ResumePlayBack()
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");
           
            await spotify.Player.ResumePlayback();
         

            _server.Dispose();
            //  Environment.Exit(0);
        }
        public static async Task setVolume(int volume)
        {

            if (Mute)
            {
                await Desmute();
            }
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
         //   Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");
                     int atualpercent =  spotify.Player.GetCurrentPlayback().Result.Device.VolumePercent.GetValueOrDefault();
            int result = atualpercent + volume;
            PlayerVolumeRequest playvolumecontext = new PlayerVolumeRequest(result);
     
            await spotify.Player.SetVolume(playvolumecontext);


            _server.Dispose();
            //  Environment.Exit(0);
        }
        public static bool IsMute()
        {
            if (Mute)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static async Task Desmute()
        {
           
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            //   Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");
        
            PlayerVolumeRequest playvolumecontext = new PlayerVolumeRequest(volumeBefore);

            await spotify.Player.SetVolume(playvolumecontext);
            Mute = false;

            _server.Dispose();

        }
        public static async Task PausePlayBack()
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");

            await spotify.Player.PausePlayback();
          
            _server.Dispose();
            //  Environment.Exit(0);
        }
        public static async Task SkipNext()
        {

            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");

            await spotify.Player.SkipNext();

            _server.Dispose();
        }
        public static async Task SkipPrevius()
        {

            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");

            await spotify.Player.SkipPrevious();

            _server.Dispose();
        }
        public static bool isClicked = true;
        public static int volumeBefore;
        private static bool Mute = false;


        public static async Task MuteDesmute()
        {
           
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");
             
           
            if (isClicked)
            {
             volumeBefore = spotify.Player.GetCurrentPlayback().Result.Device.VolumePercent.Value;
                PlayerVolumeRequest playvolumecontext = new PlayerVolumeRequest(0);
                await spotify.Player.SetVolume(playvolumecontext);
                isClicked = false;
                Mute = true;
            }
            else
            {
              
                 PlayerVolumeRequest playvolume = new PlayerVolumeRequest(volumeBefore);
                await spotify.Player.SetVolume(playvolume);
                isClicked = true;
                Mute = false;
            }

            _server.Dispose();
        }
        public static async Task PlayPlaylist(SimplePlaylist playlist)
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);
      
            var me = await spotify.UserProfile.Current();

            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");


            PlayerResumePlaybackRequest teste = new PlayerResumePlaybackRequest();
            teste.ContextUri = playlist.Uri;

              await spotify.Player.ResumePlayback(teste);
      

            _server.Dispose();
            //  Environment.Exit(0);
        }
     
        public static async Task <SimplePlaylist> getPlayListById(string id)
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");

            var playlists = await spotify.PaginateAll(await spotify.Playlists.CurrentUsers().ConfigureAwait(false));
            SimplePlaylist item = null;
            foreach (SimplePlaylist list_item in playlists)
            {

                if(list_item.Id == id)
                {
                    item = list_item;
                    break;
                }
            }

            _server.Dispose();

            return item;

        }
            public static async Task <System.Collections.Generic.IList<SpotifyAPI.Web.SimplePlaylist>> getAllPlayerList()
        {
            var json = await File.ReadAllTextAsync(CredentialsPath);
            var token = JsonConvert.DeserializeObject<PKCETokenResponse>(json);

            var authenticator = new PKCEAuthenticator(clientId!, token);
            authenticator.TokenRefreshed += (sender, token) => File.WriteAllText(CredentialsPath, JsonConvert.SerializeObject(token));

            var config = SpotifyClientConfig.CreateDefault()
              .WithAuthenticator(authenticator);

            var spotify = new SpotifyClient(config);

            var me = await spotify.UserProfile.Current();
            Debug.WriteLine($"Welcome {me.DisplayName} ({me.Id}), you're authenticated!");

            var playlists = await spotify.PaginateAll(await spotify.Playlists.CurrentUsers().ConfigureAwait(false));
            _server.Dispose();
            
            return playlists;

            
            //  Environment.Exit(0);
        }

        private static async Task StartAuthentication()
        {
            var (verifier, challenge) = PKCEUtil.GenerateCodes();

            await _server.Start();
            _server.AuthorizationCodeReceived += async (sender, response) =>
            {
                await _server.Stop();
                PKCETokenResponse token = await new OAuthClient().RequestToken(
                  new PKCETokenRequest(clientId!, response.Code, _server.BaseUri, verifier)
                );

                await File.WriteAllTextAsync(CredentialsPath, JsonConvert.SerializeObject(token));
                await Start();
            };

            var request = new LoginRequest(_server.BaseUri, clientId!, LoginRequest.ResponseType.Code)
            {
                CodeChallenge = challenge,
                CodeChallengeMethod = "S256",
                Scope = new List<string> { UserReadEmail, UserReadPlaybackPosition,UserReadCurrentlyPlaying,UserModifyPlaybackState ,UserReadPlaybackState,UserReadPlaybackState,UserReadPrivate, PlaylistReadPrivate,PlaylistModifyPublic,PlaylistModifyPrivate, Streaming,AppRemoteControl,PlaylistReadCollaborative }
            };

            Uri uri = request.ToUri();
            try
            {
                BrowserUtil.Open(uri);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to open URL, manually open: {0}", uri);
            }
        }
    }
}
