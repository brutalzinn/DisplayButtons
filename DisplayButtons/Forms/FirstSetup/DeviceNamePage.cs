using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisplayButtons.Backend.Utils;
using Microsoft.Win32;

namespace DisplayButtons.Forms.FirstSetup
{
    public partial class DeviceNamePage : PageTemplate
    {
        private const string registryAppName = "DisplayButtons";

        public static void AddApplicationToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)) {
                key.SetValue(registryAppName, "\"" + Application.ExecutablePath + "\" /s");
            }
        }

        public static void RemoveApplicationFromStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)) {
                key.DeleteValue(registryAppName, false);
            }
        }

        public override void SaveProgress()
        {
            ApplicationSettingsManager.Settings.DeviceName = textBox1.Text.Trim();
            ApplicationSettingsManager.Settings.FirstRun = false;
            if (checkBox1.Checked) {
                AddApplicationToStartup();
            } else {
                RemoveApplicationFromStartup();
            }
            ApplicationSettingsManager.SaveSettings();
        }

        public override bool CanProgress { get => !(string.IsNullOrEmpty(textBox1.Text.Trim()) && string.IsNullOrWhiteSpace(textBox1.Text.Trim())); set => base.CanProgress = value; }
        public DeviceNamePage()
        {
            InitializeComponent();
            Globals.events.On("languagechanged", (e) => {

                label1.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_LABEL1", Texts.cultereinfo);
                label2.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_LABEL2", Texts.cultereinfo);
                label4.Text = Texts.rm.GetString("DEVICENAME", Texts.cultereinfo);
                checkBox1.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_LABEL3", Texts.cultereinfo);
                label3.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_LABEL4", Texts.cultereinfo); 

            });


        }

        private void DeviceNamePage_Load(object sender, EventArgs e)
        {
            
            this.Refresh();
        
         //   checkBox1.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_LABEL4", Texts.cultereinfo);

        }
    }
}
