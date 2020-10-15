using Backend;
using Backend.Networking;
using Backend.Networking.Implementation;
using Backend.Objects;
using Backend.Utils;
using Misc;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Backend.Utils.AppSettings;

namespace DisplayButtons.Forms
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
            Initilizator.ServerThread.Start();
        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            Initilizator.ServerThread.Stop();
            Initilizator.ServerThread = new Misc.ServerThread();
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
            sb.AppendLine($"    - Port Number: {ApplicationSettingsManager.Settings.PORT}");
            sb.AppendLine($"    - Persisted Devices: {DevicePersistManager.PersistedDevices.Count}");
            sb.AppendLine($"    - Current Theme: {ApplicationSettingsManager.Settings.Theme}");
            sb.AppendLine($"    - Debug Build: {BooleanToString(isDebugBuild)}");
            sb.AppendLine($"    - OBS Nagged: {BooleanToString(ApplicationSettingsManager.Settings.OBSPluginNagged)}");
            sb.AppendLine($"    - Device Name: {ApplicationSettingsManager.Settings.DeviceName}");
            sb.AppendLine($"    - Persisted Devices: {DevicePersistManager.PersistedDevices.Count}");
            sb.AppendLine($"    - Is OBS Connected: {OBSUtils.IsConnected}");
            //sb.AppendLine($"    - TCP Server Connections: {Program.ServerThread?.TcpServer?.CurrentConnections}");
         //   sb.AppendLine($"    - TCP Server Max Connections: {Program.ServerThread?.TcpServer?.MaxConnections}");
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

                    // ApplicationSettingsManager.Settings.coluna = Convert.ToInt32(coluna.Text);
                    //   ApplicationSettingsManager.Settings.linha= Convert.ToInt32(linha.Text);
                    MatrizObject new_matriz = new MatrizObject(Convert.ToInt32(linha.Text), Convert.ToInt32(coluna.Text));
                    MainForm.Instance.CurrentDevice.CurrentProfile.Matriz = new_matriz;
                   
                    var matriz_packet = new MatrizPacket(MainForm.Instance.CurrentDevice.CurrentProfile);
                   
                con.SendPacket(matriz_packet);
                  
            }

                MainForm.Instance.MatrizGenerator(MainForm.Instance.CurrentDevice.CurrentProfile);
            }
            catch(Exception ea)
            {

                MessageBox.Show("Aguardando conexão com o device.." + ea);
            }

        }

        private void MagnetiteForm_Load(object sender, EventArgs e)
        {

            var con = MainForm.Instance.CurrentDevice.GetConnection();

            if (con != null)
            {
                linha.Text = MainForm.Instance.CurrentDevice.CurrentProfile.Matriz.Lin.ToString();
                coluna.Text = MainForm.Instance.CurrentDevice.CurrentProfile.Matriz.Column.ToString();

            }
            label6.Text = Texts.rm.GetString("MAGNETITEFORMPORTLABEL", Texts.cultereinfo);
            port_save_button.Text = Texts.rm.GetString("MAGNETITEFORMPORTBUTTON", Texts.cultereinfo); 
            modernButton2.Text = Texts.rm.GetString("IMPORTPROFILE", Texts.cultereinfo);
            modernButton1.Text = Texts.rm.GetString("EXPORTPERFIL", Texts.cultereinfo);
            modernButton11.Text = Texts.rm.GetString("PRINCIPALFOLDER", Texts.cultereinfo);
            modernButton9.Text = Texts.rm.GetString("BACKFOLDER", Texts.cultereinfo);
            port_textbox.Text = ApplicationSettingsManager.Settings.PORT.ToString();

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

        private void modernButton2_Click(object sender, EventArgs e)
        {

        }

        private void port_save_button_Click(object sender, EventArgs e)
        {
            ApplicationSettingsManager.Settings.PORT = Convert.ToInt32(port_textbox.Text);
        }
    }
}
