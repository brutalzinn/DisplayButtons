using Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.ActionHelperForms.SpotifyForms
{
    public partial class SpotifyAudioSensibility : TemplateForm
    {
        public SpotifyAudioSensibility()
        {
            InitializeComponent();

            ok_button.Text = Texts.rm.GetString("BUTTONSAVE", Texts.cultereinfo);
            Cancel_button.Text = Texts.rm.GetString("BUTTONCANCEL", Texts.cultereinfo);
          label1.Text = Texts.rm.GetString("SPOTIFYAUDIOSENSIBILYHELPERTEXT", Texts.cultereinfo); 
        }

        private void SpotifyAudioSensibility_Load(object sender, EventArgs e)
        {
           
        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);

        }
    }
}
