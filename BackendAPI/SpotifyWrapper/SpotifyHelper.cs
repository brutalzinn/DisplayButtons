using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BackendAPI.Bibliotecas.SpotifyWrapper
{
  
        public class GlobalControlSpotifyPlayList
        {
            public string Text { get; set; }
            public SimplePlaylist Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    public  static class SpotifyHelper
    {
        public static void SelectItemByValue(this ComboBox cbo, SimplePlaylist playlist)
        {

            if (playlist != null)
            {
                foreach (GlobalControlSpotifyPlayList item in cbo.Items)
                {

                    if (item.Value.Id == playlist.Id)
                    {
                        cbo.SelectedItem = item;
                        break;
                    }


                }
            }
        }
    }

}
