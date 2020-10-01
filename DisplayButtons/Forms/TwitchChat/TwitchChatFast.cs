using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
  

      
        private void TwitchChatFast_Load(object sender, EventArgs e)
        {
          
        }
 
        public void initilizeWeb(string username)
        {
            var geckoWebBrowser = new GeckoWebBrowser { Dock = DockStyle.Fill };
            Form f = new Form();
            f.Controls.Add(geckoWebBrowser);
            geckoWebBrowser.Navigate("www.google.com");
            Application.Run(f);


        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);
        }
    }
}
