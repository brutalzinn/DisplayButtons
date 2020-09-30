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
  

        public void EnsureBrowserEmulationEnabled(string exename = "MarkdownMonster.exe", bool uninstall = false)
        {

            try
            {
                using (
                    var rk = Registry.CurrentUser.OpenSubKey(
                            @"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true)
                )
                {
                    if (!uninstall)
                    {
                        dynamic value = rk.GetValue(exename);
                        if (value == null)
                            rk.SetValue(exename, (uint)11001, RegistryValueKind.DWord);
                    }
                    else
                        rk.DeleteValue(exename);
                }
            }
            catch
            {
            }
        }
        private void TwitchChatFast_Load(object sender, EventArgs e)
        {
          
        }
        private void MakeControl(string username)
        {
          
            
            

           
          
        }
        public void initilizeWeb(string username)
        {


EnsureBrowserEmulationEnabled("DisplayButtons.exe");
            
           webBrowser1.Navigate($"https://twitch.tv/popout/{username}/chat");

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
