using DisplayButtons.Bibliotecas.SpotifyWrapper;
using SpotifyAPI.Web;
using Swan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DisplayButtons.Bibliotecas.DeckEvents.FactoryForms;

namespace DisplayButtons.Forms.ActionHelperForms.SpotifyForms
{
    public partial class SpotifyPlaylists : TemplateForm
    {
        public SpotifyPlaylists()
        {
            InitializeComponent();
            save_button.Text = Texts.rm.GetString("BUTTONSAVE", Texts.cultereinfo);
            cancel_button.Text = Texts.rm.GetString("BUTTONCANCEL", Texts.cultereinfo);
            label1.Text = Texts.rm.GetString("SPOTIFYAUDIOPLAYLISTLABELHELPER", Texts.cultereinfo);

            var val = Task.Run(() => Spotify.getAllPlayerList());

 
            foreach (var item in  val.Result)
            {
                GlobalControlSpotifyPlayList teste = new GlobalControlSpotifyPlayList();
                teste.Text = item.Name;
                teste.Value = item;
                comboBox1.Items.Add(teste);

            }
          
        }

        public void SelectPlaylist(string id)
        {
            var val = Task.Run(() => Spotify.getPlayListById(id));

            SpotifyHelper.SelectItemByValue(comboBox1,val.Result);
        }

        private void SpotifyPlaylists_Load(object sender, EventArgs e)
        {

        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {

           
            CloseWithResult(DialogResult.OK);
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }
    }
}
