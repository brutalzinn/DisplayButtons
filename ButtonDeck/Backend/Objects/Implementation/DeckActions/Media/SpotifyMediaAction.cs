using ButtonDeck.Backend.Utils.Native;
using ButtonDeck.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI.Web;
using System.Net;
using System.Collections.Specialized;

namespace ButtonDeck.Backend.Objects.Implementation.DeckActions.Media
{
    public class SpotifyMediaAction : AbstractDeckAction
    {

        static bool firstTime;
        public static SpotifyAPI.Web.SpotifyClient SpotifyCliente;
        public enum StatePlay
        {
            Start,
            Stop,
            Toggle
        }
        public StatePlay PlayAction { get; set; }
        public enum MediaSpotifyKeys
        {
            [Description("Anterior")]
            Back,
            [Description("Proximo")]
            Next,
            [Description("Tocar/Pausar")]
            PlayPause,
            [Description("Parar")]
            Stop,
            [Description("Mudo")]
            VolumeOff,
            [Description("Abaixar volume")]
            VolumeMinus,
            [Description("Aumentar volume")]
            VolumePlus
        }

        [ActionPropertyInclude]
        [ActionPropertyDescription("Spotify key")]
        [ActionPropertyUpdateImageOnChanged]
        public MediaSpotifyKeys Key { get; set; } = MediaSpotifyKeys.PlayPause;

        public override AbstractDeckAction CloneAction()
        {

            return new SpotifyMediaAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.Spotify;
        }

        public override string GetActionName()
        {


            if (!firstTime)
            {
                firstTime ^= true;
                return "Start/Stop Streaming";
            }
            switch (PlayAction)
            {
                case StatePlay.Start:
                    return "Start Streaming";
                case StatePlay.Stop:
                    return "Stop Streaming";



                   
            }

            return "Start/Stop Streaming";
        }

        [Obsolete]
        public override bool OnButtonClick(DeckDevice deckDevice)
        {
            return false;
        }

        public int GetKeyFromMediaKey(MediaSpotifyKeys mediaKey)
        {
            switch (mediaKey)
            {
                case MediaSpotifyKeys.Back:
                    return 1;
                case MediaSpotifyKeys.Next:
                    return 2;
                case MediaSpotifyKeys.PlayPause:
                    return 3;
                case MediaSpotifyKeys.Stop:
                    return 4;
                case MediaSpotifyKeys.VolumeOff:
                    return 5;
                case MediaSpotifyKeys.VolumeMinus:
                    return 7;
                case MediaSpotifyKeys.VolumePlus:
                    return 7;
            }
            return 0;
        }
        public void GetClientCredentialsAuthToken()
        {
            var spotifyClient = "";
            var spotifySecret = "";

            var webClient = new WebClient();

            var postparams = new NameValueCollection();
            postparams.Add("grant_type", "client_credentials");

            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{spotifyClient}:{spotifySecret}"));
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authHeader);

            var tokenResponse = webClient.UploadValues("https://accounts.spotify.com/api/token", postparams);

            var textResponse = Encoding.UTF8.GetString(tokenResponse);
        }
        public override void OnButtonDown(DeckDevice deckDevice)
        {

        }
       
     
    

        public override void OnButtonUp(DeckDevice deckDevice)
        {
            int playpause = 0;
            var key = GetKeyFromMediaKey(Key);
            if (key != 0)
            {
                try
                    {
                if (key == 1)
                {
                

                }
                if ( key == 2)
                {
                    // SpotifyCliente.Player.SkipNext();

                }
              if(key == 4)
                {


                    // SpotifyCliente.Player.PausePlayback();
                }
                if (key == 3)
                {
                  
                   
                        switch (PlayAction)
                        {
                            case StatePlay.Start:
                                //       API.resume();




                             //  await SpotifyCliente.Player.ResumePlayback();

                                PlayAction = StatePlay.Stop;
                                // is the same as



                                break;
                            case StatePlay.Stop:
                             //   await SpotifyCliente.Player.PausePlayback();
                                PlayAction = StatePlay.Start;
                                //   spotify.Player.PausePlayback();
                                //       API.pause();
                                break;
                        }
                    }
                
                
             
                   
                
                }
                catch (Exception eee)
                    {
                

                    }
                //   NativeKeyHandler.ClickKey(new[] { key });
            }
        }

        public override DeckImage GetDefaultItemImage()
        {
            var img = GetKey(Key);
            if (img != null)
                return new DeckImage(img);
            return base.GetDefaultItemImage();
        }

        private Bitmap GetKey(MediaSpotifyKeys key)
        {
            switch (key)
            {
                case MediaSpotifyKeys.Back:
                    return Resources.img_item_media_back;
                case MediaSpotifyKeys.Next:
                    return Resources.img_item_media_next;
                case MediaSpotifyKeys.PlayPause:
                    return Resources.img_item_media_playpause;
                case MediaSpotifyKeys.Stop:
                    return Resources.img_item_media_stop;
                case MediaSpotifyKeys.VolumeOff:
                    return Resources.img_item_media_volumeoff;
                case MediaSpotifyKeys.VolumeMinus:
                    return Resources.img_item_media_volumedown;
                case MediaSpotifyKeys.VolumePlus:
                    return Resources.img_item_media_volumeup;
                default:
                    return null;
            }
        }

    }
}
