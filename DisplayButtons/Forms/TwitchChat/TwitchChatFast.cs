using DisplayButtons.Bibliotecas.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DisplayButtons.Forms.TwitchChat
{
    public partial class TwitchChatFast : TemplateForm
    {
        public TwitchChatFast()
        {
            InitializeComponent();

        }
        public WebBrowser wb;



        private void TwitchChatFast_Load(object sender, EventArgs e)
        {
          
        }
 
        public void initilizeWeb(string username)
        {

       wb = new WebBrowser();

            panel1.Controls.Add(wb);
            wb.Dock = DockStyle.Fill;


            wb.Navigate($"https://www.twitch.tv/popout/{username}/chat", false);
            


        }
        private void CloseWithResult(DialogResult result)
        {
            wb.Dispose();
               DialogResult = result;
            Close();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
          //  WinInetHelper.EndBrowserSession();
            CloseWithResult(DialogResult.OK);
        }
    }
}
