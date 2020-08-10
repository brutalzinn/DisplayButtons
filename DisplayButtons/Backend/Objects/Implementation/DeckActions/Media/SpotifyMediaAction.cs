using DisplayButtons.Backend.Utils.Native;
using DisplayButtons.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.Media
{
    public class SpotifyMediaAction : AbstractDeckAction
    {


        public enum MediaSpotifyKeys
        {
            [Description("Anterior")]
            Back,
            [Description("Proximp")]
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
            return "Spotify integration";
        }

        [Obsolete]
        public override bool OnButtonClick(DeckDevice deckDevice)
        {
            return false;
        }

        public Keys GetKeyFromMediaKey(MediaSpotifyKeys mediaKey)
        {
            switch (mediaKey)
            {
                case MediaSpotifyKeys.Back:
                    return Keys.MediaPreviousTrack;
                case MediaSpotifyKeys.Next:
                    return Keys.MediaPreviousTrack;
                case MediaSpotifyKeys.PlayPause:
                    return Keys.MediaPlayPause;
                case MediaSpotifyKeys.Stop:
                    return Keys.MediaStop;
                case MediaSpotifyKeys.VolumeOff:
                    return Keys.VolumeMute;
                case MediaSpotifyKeys.VolumeMinus:
                    return Keys.VolumeDown;
                case MediaSpotifyKeys.VolumePlus:
                    return Keys.VolumeUp;
            }
            return Keys.None;
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {

        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
            var key = GetKeyFromMediaKey(Key);
            if (key != Keys.None)
            {
                NativeKeyHandler.ClickKey(new[] { key });
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
