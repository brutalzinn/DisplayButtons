//#define FORCE_SILENCE

using DisplayButtons.Forms;
using ScribeBot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using SharpAdbClient;
using System.Net;
using System.Threading;
using DisplayButtons.BackendAPI.Objects;
using DisplayButtons.Forms.ActionHelperForms;
using DisplayButtons.Bibliotecas.DeckEvents;
using MoreLinq;
using System.Reflection;
using Microsoft.Win32;
using DisplayButtons.Bibliotecas.Helpers;
using BackendAPI.Utils;
using BackendAPI;
using Misc;
using WebShard.Filtering;
using WebShard.Ioc;
using WebShard.Routing;

using BackendAPI.Objects;
using WebShard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace DisplayButtons
{

    
    static class Program
    {
        

        public static bool Silent { get; set; } = false;
        private static string errorText = "";
        private const string errorFileName = "errors.log";
        private static Mutex mutex = null;


        public static int mode { get; set; }
        public static bool SuccessfulServerStart { get; set; } = false;
        public static StartServerResult AdbResult { get; set; }
        public static AdbServer Adbserver { get; set; }
        public static DeviceMonitor monitor { get; set; }
        public static AdbClient client { get; set; }
        public static List<DeviceData> device_list = new List<DeviceData>();

        public static void EnsureBrowserEmulationEnabled(string exename = "MarkdownMonster.exe", bool uninstall = false)
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
        public static Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) 
        {
        
          Trace.Listeners.Add(new TextWriterTraceListener(errorFileName));
           Trace.AutoFlush = true;

            if (args.Any(c => c.ToLower() == "/armobs"))
            {
                if (args.Length == 1)
                {
                 
                    var obs32List = Process.GetProcessesByName("obs32");
                    var obs64List = Process.GetProcessesByName("obs64");
                    if (obs32List.Length == 0 && obs64List.Length == 0)
                    {
                        //No OBS found. Cancel operation.
                        File.Delete(OBSUtils.obswszip);
                    
                        return;
                    }
                    List<Process> obsProcesses = new List<Process>();
                    obsProcesses.AddRange(obs32List);
                    obsProcesses.AddRange(obs64List);

                    if (obsProcesses.Count != 1)
                    {
                        //Multiple OBS instances found. Cancel operation.
                        File.Delete(OBSUtils.obswszip);
                       
                        return;
                    }
                    var obsProcess = obsProcesses.First();

                    string path = OBSUtils.GetProcessPath(obsProcess.Id); 
                    
         
                    string zipTempPath = Path.GetFileNameWithoutExtension(OBSUtils.obswszip);
                 
                    OBSUtils.ExtractZip(OBSUtils.obswszip, zipTempPath);

                  
                    OBSUtils.DirectoryCopy(zipTempPath, OBSUtils.GetPathFromOBSExecutable(path), true);
          
                    File.Delete(OBSUtils.obswszip);
                    Directory.Delete(zipTempPath, true);
              
                    var obsGlobalFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obs-studio", "global.ini");
           
                    if (File.Exists(obsGlobalFilePath) && !File.ReadAllText(obsGlobalFilePath).Contains("[WebsocketAPI]"))
                    {

                        Trace.WriteLine("File exist and not contain web socket.");
                        while (!obsProcess.HasExited)
                        {
                            
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(Texts.rm.GetString("OBSINTEGRATIONINSTALL", Texts.cultereinfo));
                            sb.AppendLine("");
                            sb.AppendLine(Texts.rm.GetString("OBSINTEGRATIONAPPLY", Texts.cultereinfo));
                            MessageBox.Show(sb.ToString());
                        }

                        using (StreamWriter outputFile = new StreamWriter(obsGlobalFilePath))
                        {
                            outputFile.WriteLine("");
                            outputFile.WriteLine("[WebsocketAPI]");
                            outputFile.WriteLine("ServerPort=4444");
                            outputFile.WriteLine("DebugEnabled=false");
                            outputFile.WriteLine("AlertsEnabled=false");
                            outputFile.WriteLine("AuthRequired=false");
                        }

                        bool shouldRepeat = true;
                        while (shouldRepeat)
                        {
                            var obs32List2 = Process.GetProcessesByName("obs32");
                            var obs64List2 = Process.GetProcessesByName("obs64");

                            shouldRepeat = obs32List2.Length == 0 && obs64List2.Length == 0;
                            if (!shouldRepeat) break;
                     
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine(Texts.rm.GetString("OBSINTEGRATIONINSTALLSUCESSFULL", Texts.cultereinfo));
                            sb.AppendLine("");
                            sb.AppendLine(Texts.rm.GetString("OBSINTEGRATIONCONFIRMOBS", Texts.cultereinfo));
                            MessageBox.Show(sb.ToString());
                        }

                    }

                    return;
                }
            }

#if FORCE_SILENCE
    Silent = true;
#else
            Silent = args.Any(c => c.ToLower() == "/s");
#endif
            const string appName = "DisplayButtons";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                
                //app is already running! Exiting the application  
               if ( MessageBox.Show(Texts.rm.GetString("ALREADYDISPLAYBUTTONISOPEN", Texts.cultereinfo), Texts.rm.GetString("ALREADYDISPLAYBUTTONISOPENTITLE", Texts.cultereinfo), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {      
                    var myapp = Process.GetProcessesByName(Assembly.GetCallingAssembly().GetName().Name);

                    List<Process> obsProcesses = new List<Process>();
                    obsProcesses.AddRange(myapp);


                   if( obsProcesses.Count > 1)
                    {
                        var last = obsProcesses.First();
                        last.Kill();
                    }
                }
                //return;
            }
           

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
         

            EventXml.LoadSettings();

            ApplicationSettingsManager.LoadSettings();
             Texts.initilizeLang();         
            errorText = String.Format(Texts.rm.GetString("INTEGRATIONERROROCURRED", Texts.cultereinfo), errorFileName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
      OBSUtils.PrepareOBSIntegration();
            if (ApplicationSettingsManager.Settings.FirstRun)
            {
                FirstSetupForm firstRunForm = new FirstSetupForm();
                Application.Run(firstRunForm);
                if (!firstRunForm.FinishedSetup) return;
            }
    
     


            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.ActionHelperForms.MainFormMenuOption")) as Form;
            Debug.WriteLine("DIRECTORY: " + Directory.GetCurrentDirectory());

            CreateWebHostBuilder(args).Build().RunAsync();

            //   server.Stop();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Initilizator.mode = 0;
     Initilizator.ServerThread = new ServerThread();
                Initilizator.ServerThread.Start();
            

                Debug.WriteLine("MODO SOCKET CLIENT");

 

            }
            else
            {
               // Silent = true;
                Debug.WriteLine("MODO USB");
                Initilizator.mode = 1;
              
                Adbserver = new AdbServer();
              
  Adbserver.StartServer(Path.Combine(Application.StartupPath , @"Data\adb\adb.exe"), restartServerIfNewer: true);
           
                        monitor = new DeviceMonitor(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)));
                client = new AdbClient(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort), Factories.AdbSocketFactory);

                monitor.DeviceConnected += MainForm.DeviceAdbConnected;
                monitor.DeviceDisconnected += MainForm.DeviceAdbDisconnected;
                monitor.Start();



                if (client.GetDevices().Count == 1)
                {


                    Debug.WriteLine("ONE DEVICE");



                    //   client.ExecuteRemoteCommand("am start -a android.intent.action.VIEW -e mode 1 net.nickac.DisplayButtons/.MainActivity", client.GetDevices().First(), null);

                       DevicePersistManager.PersistUsbMode(client.GetDevices().First());
                    //      client.CreateForward(client.GetDevices().First(), "tcp:5095", "tcp:5095", true);

                    Initilizator.ClientThread = new ClientThread();
                    Initilizator.ClientThread.Start();

                }
                else
                {

                    Initilizator.ClientThread = new ClientThread();
                  


                }

                }
Application.Run(new MainForm());

             
            //Application.Run(new MainFormMenuOption());
            OBSUtils.Disconnect();
            if (Initilizator.mode == 1)
            {

             
                foreach (var device in client.GetDevices().ToList())
            {
                    
             //   client.ExecuteRemoteCommand("am force-stop net.nickac.DisplayButtons", device, null);
                   // client.ExecuteRemoteCommand("kill-server", device, null);

                 //   client.KillAdb();
            }
              

            }

         //   client.KillAdb();   
            EventXml.SaveSettings();
           ApplicationSettingsManager.SaveSettings();
             DevicePersistManager.SaveDevices();
         
            NetworkChange.NetworkAddressChanged -= NetworkChange_NetworkAddressChanged;
            NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAddressChanged;
            Trace.Flush();
        }
    
        private static void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            if(mode ==0)
            {
   Initilizator.ServerThread.Stop();
                Initilizator.ServerThread = new ServerThread();
                Initilizator.ServerThread.Start();

            }
            else
            {
                Initilizator.ClientThread.Stop();
                Initilizator.ClientThread = new ClientThread();
                Initilizator.ClientThread.Start();

            }
         
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                MessageBox.Show(errorText);
                Trace.WriteLine("An error occured.");
                Trace.WriteLine($"Timestamp: [Local:{DateTime.Now}; UTC: {DateTime.UtcNow}].");
                Trace.WriteLine("Exception:");
                Trace.WriteLine("==================");
                Trace.Indent();
                Trace.TraceError(ex.ToString());
                Trace.Unindent();
                Trace.WriteLine("==================");
                Trace.WriteLine("");

            }
        }



        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(errorText);
            Trace.WriteLine("An error occured. (Thread exception)");
            Trace.WriteLine($"Timestamp: [Local:{DateTime.Now}; UTC: {DateTime.UtcNow}].");
            Trace.WriteLine("Exception:");
            Trace.WriteLine("==================");
            Trace.Indent();
            Trace.TraceError(e.Exception.ToString());
            Trace.Unindent();
            Trace.WriteLine("==================");
            Trace.WriteLine("");
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
              WebHost.CreateDefaultBuilder(args)
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseStartup<Startup>();
    }
}