﻿using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Backend.Objects.Implementation.DeckActions.General;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Controls;
using DisplayButtons.Misc;
using DisplayButtons.Properties;

using DisplayButtons.Backend.Networking;

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
using static DisplayButtons.Backend.Objects.AbstractDeckAction;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using static DisplayButtons.Backend.Utils.DevicePersistManager;
using DisplayButtons.Backend.Networking.TcpLib;
using static DisplayButtons.Backend.Utils.AppSettings;

namespace DisplayButtons.Forms
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
            sb.AppendLine("DisplayButtons Dump:");
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
              
              
                     var Matriz = new MatrizPacket();
                con.SendPacket(Matriz);
                  
            }

                MainForm.Instance.MatrizGenerator();
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

            modernButton2.Text = Texts.rm.GetString("IMPORTPROFILE", Texts.cultereinfo);
            modernButton1.Text = Texts.rm.GetString("EXPORTPERFIL", Texts.cultereinfo);
            modernButton11.Text = Texts.rm.GetString("PRINCIPALFOLDER", Texts.cultereinfo);
            modernButton9.Text = Texts.rm.GetString("BACKFOLDER", Texts.cultereinfo);


            modernButton8.Text = Texts.rm.GetString("MATRIZCHANGER", Texts.cultereinfo);

            modernButton4.Text = Texts.rm.GetString("STARTTCPSERVER", Texts.cultereinfo);
            modernButton3.Text = Texts.rm.GetString("STOPTCPSERVER", Texts.cultereinfo);
            appBar1.Text = Texts.rm.GetString("APPLICATION_MENU", Texts.cultereinfo);
            label1.Text = Texts.rm.GetString("APPLICATIONNAME_DESCRIPTION", Texts.cultereinfo);
            modernButton3.Text = Texts.rm.GetString("STOPTCPSERVER", Texts.cultereinfo);
            modernButton10.Text = Texts.rm.GetString("DEVELOPERMODE", Texts.cultereinfo);
            modernButton10.Text = Texts.rm.GetString("DEVELOPERMODE", Texts.cultereinfo);
            modernButton6.Text = Texts.rm.GetString("OBSCONNECTION", Texts.cultereinfo);
            modernButton5.Text = Texts.rm.GetString("STOPOBSCONNECTION", Texts.cultereinfo);



            imageModernButton2.Text = Texts.rm.GetString("MAGNETITEBROWSERFOLDERBUTTON", Texts.cultereinfo);
            imageModernButton1.Text = Texts.rm.GetString("MAGNETITEAUTOMINIMIZER", Texts.cultereinfo);


            label2.Text = Texts.rm.GetString("MAGNETITECOOLSATALHOS", Texts.cultereinfo);

            loadSettingsPanel(shadedPanel1, ApplicationSettingsManager.Settings.isAutoMinimizer);
            loadSettingsPanel(shadedPanel2, ApplicationSettingsManager.Settings.isFolderBrowserEnabled);
            loadSettingsPanel(panel7, ApplicationSettingsManager.Settings.isDevelopermode);
        }
        // painel // settings
        public void loadSettingsPanel(Control parent, bool settings)
        {
            string status = "";
            switch (settings)
            {

                case true:
                    status = Texts.rm.GetString("ISTHISENABLED", Texts.cultereinfo); ;
                    break;

                case false:
                    status = Texts.rm.GetString("ISTHISDISABLED", Texts.cultereinfo); ;
                    break;
            }


            parent.Controls.OfType<Label>().All((c) => {
              c.Text = Texts.rm.GetString("ISTHISSTATUS", Texts.cultereinfo) + ": " + status;
                return true;
            });


        }

      

        private void ModernButton9_Click(object sender, EventArgs e)
        {

            var keyInfo = new KeyInfoAppSettingsGlobal(ApplicationSettingsManager.Settings.keyBackFolder.ModifierKeys, ApplicationSettingsManager.Settings.keyBackFolder.Keys);
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.ActionHelperForms.MagnetiteControlsSelector")) as Form;

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
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.ActionHelperForms.MagnetiteControlsSelector")) as Form;

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
                    status = Texts.rm.GetString("ISTHISENABLED", Texts.cultereinfo); ;
                    break;

                case false:
                    status = Texts.rm.GetString("ISTHISDISABLED", Texts.cultereinfo); ;
                    break;
            }

            label3.Text = Texts.rm.GetString("ISTHISSTATUS", Texts.cultereinfo) + ": " + status;

            MainForm.Instance.ChangeDeveloperMode();
        }

        private void ImageModernButton1_Click(object sender, EventArgs e)
        {
            string status = "";
            if (ApplicationSettingsManager.Settings.isAutoMinimizer == false)
            {
                ApplicationSettingsManager.Settings.isAutoMinimizer = true;

            }
            else
            {
                ApplicationSettingsManager.Settings.isAutoMinimizer = false;

            }
            switch (ApplicationSettingsManager.Settings.isAutoMinimizer)
            {

                case true:
                    status = Texts.rm.GetString("ISTHISENABLED", Texts.cultereinfo); 
                    break;

                case false:
                    status = Texts.rm.GetString("ISTHISDISABLED", Texts.cultereinfo); 
                    break;
            }

            label4.Text = Texts.rm.GetString("ISTHISSTATUS", Texts.cultereinfo) + ": " + status;
        }

        private void ImageModernButton2_Click(object sender, EventArgs e)
        {
            string status = "";
            if (ApplicationSettingsManager.Settings.isFolderBrowserEnabled == false)
            {
                ApplicationSettingsManager.Settings.isFolderBrowserEnabled = true;

            }
            else
            {
                ApplicationSettingsManager.Settings.isFolderBrowserEnabled = false;

            }
            switch (ApplicationSettingsManager.Settings.isFolderBrowserEnabled)
            {

                case true:
                    status = Texts.rm.GetString("ISTHISENABLED", Texts.cultereinfo);
                    break;

                case false:
                    status = Texts.rm.GetString("ISTHISDISABLED", Texts.cultereinfo);
                    break;
            }

            label5.Text = Texts.rm.GetString("ISTHISSTATUS", Texts.cultereinfo) + ": " + status;
        }
    }
}