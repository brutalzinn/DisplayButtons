using DisplayButtons.Properties;
using DisplayButtons.Backend.Utils.Native;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.Diagnostics;
using System.Linq;
using SpotifyAPI.Web;
using DisplayButtons.Bibliotecas.SpotifyWrapper;
using System.Threading.Tasks;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.General
{
    public class SpotifyAction : AbstractDeckAction
    {
 
        public enum SpotifyMediaKeys
        {


            [Description("Back")]
            Back,
            [Description("Next")]
            Next,
            [Description("Play/Pause")]
            PlayPause,
            [Description("Stop")]
            Stop,
            [Description("Volume Off")]
            VolumeOff,
            [Description("Volume Down")]
            VolumeMinus,
            [Description("Volume Up")]
            VolumePlus,
            [Description("Play Playlist")]
            PlayList
        }

        public string PlayListId;
        [ActionPropertyInclude]
        [ActionPropertyDescription("Config Volume Sensibility")]
        [ActionPropertyUpdateImageOnChanged]
        public int VolumeSensibility { get; set; } = 10;

        [ActionPropertyInclude]
        [ActionPropertyDescription("Config Playlist")]
        [ActionPropertyUpdateImageOnChanged]
         public string ConfigPlay { get; set; }

        [ActionPropertyInclude]
        [ActionPropertyDescription("Media Key")]
        [ActionPropertyUpdateImageOnChanged]
        public SpotifyMediaKeys Key { get; set; } = SpotifyMediaKeys.PlayPause;

        public override AbstractDeckAction CloneAction()
        {
            return new SpotifyAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.Misc;
        }

        public override string GetActionName()
        {
            return Texts.rm.GetString("DECKMISCSPOTIFY", Texts.cultereinfo);
        }

        [Obsolete]
        public override bool OnButtonClick(DeckDevice deckDevice)
        {
            return false;
        }

        public Keys GetKeyFromMediaKey(SpotifyMediaKeys mediaKey)
        {
            switch (mediaKey) {
                case SpotifyMediaKeys.Back:
                    return Keys.MediaPreviousTrack;
                case SpotifyMediaKeys.Next:
                    return Keys.MediaPreviousTrack;
                case SpotifyMediaKeys.PlayPause:
                    return Keys.MediaPlayPause;
                case SpotifyMediaKeys.Stop:
                    return Keys.MediaStop;
                case SpotifyMediaKeys.VolumeOff:
                    return Keys.VolumeMute;
                case SpotifyMediaKeys.VolumeMinus:
                    return Keys.VolumeDown;
                case SpotifyMediaKeys.VolumePlus:
                    return Keys.VolumeUp;
            }
            return Keys.None;
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {

        }

       
        public override void OnButtonUp(DeckDevice deckDevice)
        { 
            Task.WaitAll( Spotify.Auth());
            switch (Key)
            {
                case SpotifyMediaKeys.PlayPause:

                    
                    Task.WaitAll(Spotify.ResumePlayBack());
                    break;
                case SpotifyMediaKeys.Stop:

                 
                    Task.WaitAll(Spotify.PausePlayBack());
                    break;

                case SpotifyMediaKeys.VolumePlus:


                    Task.WaitAll(Spotify.setVolume(VolumeSensibility));
                    break;
                case SpotifyMediaKeys.VolumeMinus:


                    Task.WaitAll(Spotify.setVolume(-VolumeSensibility));
                    break;
                case SpotifyMediaKeys.PlayList:
                    var val = Task.Run(() => Spotify.getPlayListById(PlayListId));
                    Task.WaitAll(Spotify.PlayPlaylist(val.Result));
                    break;
                case SpotifyMediaKeys.Next:
             
                    Task.WaitAll(Spotify.SkipNext());
                    break;
                case SpotifyMediaKeys.Back:
                    Task.WaitAll(Spotify.SkipPrevius());
                    break;
                case SpotifyMediaKeys.VolumeOff:
                    Task.WaitAll(Spotify.MuteDesmute());
                    break;

            }
        }
        public void VolumeSensibilityHelper()
        {
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.ActionHelperForms.SpotifyForms.SpotifyAudioSensibility")) as Form;
            form.textBox1.Text = VolumeSensibility.ToString();
            if (form.ShowDialog() == DialogResult.OK)
            {
                int result = Convert.ToInt32(form.textBox1.Text);
                if( result <= 100)
                {
VolumeSensibility = result;

                }
                
            }
            else
            {
                form.Close();
            }


        }
        public void ConfigPlayHelper()
        {
      
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.ActionHelperForms.SpotifyForms.SpotifyPlaylists")) as Form;
            form.SelectPlaylist(PlayListId);
            if (form.ShowDialog() == DialogResult.OK)
            {


                PlayListId = ((GlobalControlSpotifyPlayList)form.comboBox1.SelectedItem).Value.Id;
            }
            else
            {
                form.Close();
            }
          
        }

        public override DeckImage GetDefaultItemImage()
        {
            var img = GetKey(Key);
            if (img != null)
                return new DeckImage(img);
            return base.GetDefaultItemImage();
        }

        private Bitmap GetKey(SpotifyMediaKeys key)
        {
            switch (key) {
                case SpotifyMediaKeys.Back:
                    return Resources.img_item_media_back;
                case SpotifyMediaKeys.Next:
                    return Resources.img_item_media_next;
                case SpotifyMediaKeys.PlayPause:
                    return Resources.img_item_media_playpause;
                case SpotifyMediaKeys.Stop:
                    return Resources.img_item_media_stop;
                case SpotifyMediaKeys.VolumeOff:
                    return Resources.img_item_media_volumeoff;
                case SpotifyMediaKeys.VolumeMinus:
                    return Resources.img_item_media_volumedown;
                case SpotifyMediaKeys.VolumePlus:
                    return Resources.img_item_media_volumeup;
                default:
                    return null;
            }
        }
    }
}
