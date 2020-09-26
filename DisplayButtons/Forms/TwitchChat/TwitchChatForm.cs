
using DisplayButtons.Bibliotecas.TwitchWrapper;
using OpenQA.Selenium.Remote;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DisplayButtons.Forms.TwitchChat
{
    public partial class TwitchChatForm : TemplateForm
    {
        public TwitchChatForm()
        {
            InitializeComponent();
            TwitchWrapper.StartTwitchApi();
           

        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }
        private void TwitchChatForm_Load(object sender, EventArgs e)
        {
            
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);
        }
    }
}
