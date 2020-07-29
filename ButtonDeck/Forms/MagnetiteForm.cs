using ButtonDeck.Backend.Networking.Implementation;
using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ButtonDeck.Backend.Networking.Implementation;
using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Objects.Implementation;
using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Backend.Utils;
using ButtonDeck.Controls;
using ButtonDeck.Misc;
using ButtonDeck.Properties;

using ButtonDeck.Backend.Networking;

using NickAc.ModernUIDoneRight.Controls;
using NickAc.ModernUIDoneRight.Objects;
using ScribeBot;
using ScribeBot.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static ButtonDeck.Backend.Objects.AbstractDeckAction;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using static ButtonDeck.Backend.Utils.DevicePersistManager;
using ButtonDeck.Backend.Networking.TcpLib;
using static ButtonDeck.Backend.Utils.AppSettings;

namespace ButtonDeck.Forms
{
    public partial class MagnetiteForm : TemplateForm
    {
        bool isDebugBuild;
        private static MagnetiteForm instance;

        public static MagnetiteForm Instance
        {
            get
            {
                return instance;
            }
        }
        public MagnetiteForm()
        {
            InitializeComponent();
#if DEBUG
            isDebugBuild = true;
#endif
        }

        private void ModernButton4_Click(object sender, EventArgs e)
        {
            Program.ServerThread.Start();
        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            Program.ServerThread.Stop();
            Program.ServerThread = new Misc.ServerThread();
        }

        private void ModernButton6_Click(object sender, EventArgs e)
        {
            OBSUtils.ConnectToOBS();
        }

        private void ModernButton5_Click(object sender, EventArgs e)
        {
            OBSUtils.Disconnect();
        }

        private void ModernButton7_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ButtonDeck Dump:");
            sb.AppendLine($"    - Protocol Version: {Constants.PROTOCOL_VERSION}");
            sb.AppendLine($"    - Port Number: {Constants.PORT_NUMBER}");
            sb.AppendLine($"    - Persisted Devices: {DevicePersistManager.PersistedDevices.Count}");
            sb.AppendLine($"    - Current Theme: {ApplicationSettingsManager.Settings.Theme}");
            sb.AppendLine($"    - Debug Build: {BooleanToString(isDebugBuild)}");
            sb.AppendLine($"    - OBS Nagged: {BooleanToString(ApplicationSettingsManager.Settings.OBSPluginNagged)}");
            sb.AppendLine($"    - Device Name: {ApplicationSettingsManager.Settings.DeviceName}");
            sb.AppendLine($"    - Persisted Devices: {DevicePersistManager.PersistedDevices.Count}");
            sb.AppendLine($"    - Is OBS Connected: {OBSUtils.IsConnected}");
            //sb.AppendLine($"    - TCP Server Connections: {Program.ServerThread?.TcpServer?.CurrentConnections}");
            sb.AppendLine($"    - TCP Server Max Connections: {Program.ServerThread?.TcpServer?.MaxConnections}");
            for (int i = 0, persistedCount = DevicePersistManager.PersistedDevices.Count; i < persistedCount; i++) {
                var device = DevicePersistManager.PersistedDevices[i];
                sb.AppendLine($"        - {i}: {device.DeviceName} [{device.DeviceGuid}]");
            }


            MessageBox.Show(sb.ToString());
        }

        private string BooleanToString(bool v)
        {
            return v ? "true" : "false";
        }
        public DeckDevice CurrentDevice { get; set; }
        private void ModernButton8_Click(object sender, EventArgs e)
        {
            try
            {



                var con = MainForm.Instance.CurrentDevice.GetConnection() ;
           
                if (con != null)
            {
            //  MainForm.ButtonCreator();
                    
              ApplicationSettingsManager.Settings.coluna = Convert.ToInt32(coluna.Text);
                ApplicationSettingsManager.Settings.linha= Convert.ToInt32(linha.Text);
                ApplicationSettingsManager.SaveSettings();
              
                     var Matriz = new MatrizPacket();
                con.SendPacket(Matriz);
            }
            }
            catch(Exception ea)
            {

                MessageBox.Show("Aguardando conexão com o device.." + ea);
            }

        }

        private void MagnetiteForm_Load(object sender, EventArgs e)
        {
            coluna.Text = ApplicationSettingsManager.Settings.coluna.ToString();
            linha.Text = ApplicationSettingsManager.Settings.linha.ToString();
        }

        private void ModernButton9_Click(object sender, EventArgs e)
        {

            var keyInfo = new KeyInfoAppSettingsGlobal(ApplicationSettingsManager.Settings.keyBackFolder.ModifierKeys, ApplicationSettingsManager.Settings.keyBackFolder.Keys);
            dynamic form = Activator.CreateInstance(UsbMode.FindType("ButtonDeck.Forms.ActionHelperForms.MagnetiteControlsSelector")) as Form;

            var execAction = new AppSettings() as AppSettings;
            execAction.keyBackFolder = ApplicationSettingsManager.Settings.keyBackFolder;
            form.mode = 1;
            form.ModifiableAction = execAction;
         
            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplicationSettingsManager.Settings.keyBackFolder = form.ModifiableAction.keyBackFolder;

            }
            else
            {
                ApplicationSettingsManager.Settings.keyBackFolder = keyInfo;
            }
        }

        private void ModernButton11_Click(object sender, EventArgs e)
        {

            
            var keyInfo = new KeyInfoAppSettingsGlobal(ApplicationSettingsManager.Settings.keyMainFolder.ModifierKeys, ApplicationSettingsManager.Settings.keyMainFolder.Keys);
            dynamic form = Activator.CreateInstance(UsbMode.FindType("ButtonDeck.Forms.ActionHelperForms.MagnetiteControlsSelector")) as Form;

            var execAction = new AppSettings() as AppSettings;
            execAction.keyMainFolder = ApplicationSettingsManager.Settings.keyMainFolder;
            form.mode = 0;
            form.ModifiableAction = execAction;
          
            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplicationSettingsManager.Settings.keyMainFolder = form.ModifiableAction.keyMainFolder;

            }
            else
            {
                ApplicationSettingsManager.Settings.keyMainFolder = keyInfo;
            }
        }

        private void ModernButton10_Click(object sender, EventArgs e)
        {
            string status = "";
            if (ApplicationSettingsManager.Settings.isDevelopermode == false)
            {
                ApplicationSettingsManager.Settings.isDevelopermode = true;

            }
            else
            {
                ApplicationSettingsManager.Settings.isDevelopermode = false;

            }
            switch (ApplicationSettingsManager.Settings.isDevelopermode)
            {

                case true:
                    status = "Ativo";
                    break;

                case false:
                    status = "Desativado";
                    break;
            }

            label2.Text = "Modo: " + status;

            MainForm.Instance.ChangeDeveloperMode();
        }
    }
}
