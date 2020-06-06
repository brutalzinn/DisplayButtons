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

namespace ButtonDeck.Forms
{
    public partial class MagnetiteForm : TemplateForm
    {
        bool isDebugBuild;

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

        private void ModernButton8_Click(object sender, EventArgs e)
        {
       
            var con = MainForm.Instance.CurrentDevice.GetConnection();
            if (con != null)
            {
                ApplicationSettingsManager.Settings.coluna = Convert.ToInt32(coluna.Text);
                ApplicationSettingsManager.Settings.linha= Convert.ToInt32(linha.Text);
                ApplicationSettingsManager.SaveSettings();
           
                var Matriz = new MatrizPacket();
                con.SendPacket(Matriz);
            }

        }

        private void MagnetiteForm_Load(object sender, EventArgs e)
        {
            coluna.Text = ApplicationSettingsManager.Settings.coluna.ToString();
            linha.Text = ApplicationSettingsManager.Settings.linha.ToString();
        }
    }
}
