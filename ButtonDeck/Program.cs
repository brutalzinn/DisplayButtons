﻿//#define FORCE_SILENCE

using ButtonDeck.Forms;
using ButtonDeck.Misc;
using ButtonDeck.Backend.Utils;
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

namespace ButtonDeck
{

    
    static class Program
    {
        

        public static bool Silent { get; set; } = false;
        private static string errorText = "";
        private const string errorFileName = "errors.log";
        public static ServerThread ServerThread { get; set; }
        public static ClientThread ClientThread { get; set; }
        public static int mode { get; set; }
        public static bool SuccessfulServerStart { get; set; } = false;
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

                        while (!obsProcess.HasExited)
                        {

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("OBS plugin install completed!");
                            sb.AppendLine("");
                            sb.AppendLine("Please close OBS and click OK to apply the final touches.");
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
                            sb.AppendLine("OBS plugin install successfully!");
                            sb.AppendLine("");
                            sb.AppendLine("Please open OBS and click OK to continue to the app.");
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
            errorText = $"An error occured! And it was saved to a file called {errorFileName}." + Environment.NewLine + "Please send this to the developer of the application.";

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ApplicationSettingsManager.LoadSettings();
            DevicePersistManager.LoadDevices();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (ApplicationSettingsManager.Settings.FirstRun)
            {
                FirstSetupForm firstRunForm = new FirstSetupForm();
                Application.Run(firstRunForm);
                if (!firstRunForm.FinishedSetup) return;
            }

            OBSUtils.PrepareOBSIntegration();


            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAddressChanged;
            // começo da implementação do host usb
            dynamic form = Activator.CreateInstance(FindType("ButtonDeck.Forms.ActionHelperForms.MainFormMenuOption")) as Form;
  if (form.ShowDialog() == DialogResult.OK)
            {
                mode = 0;
     ServerThread = new ServerThread();
            ServerThread.Start();
                Application.Run(new MainForm());
                ServerThread.Stop();
                Debug.WriteLine("MODO SOCKET CLIENT");

                //     Application.Run(new MainForm());


            }
            else
            {

                Debug.WriteLine("MODO USB");
                mode = 1;
                AdbServer server = new AdbServer();
      
                var result = server.StartServer(Application.StartupPath + @"\Data\adb\adb.exe", restartServerIfNewer: true);

                var monitor = new DeviceMonitor(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)));
              
              
                var client = new AdbClient(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort), Factories.AdbSocketFactory);

                //  var devices = AdbClient.Instance.GetDevices();
                foreach (var device in client.GetDevices())
                {
 client.CreateForward(device, "tcp:5095", "tcp:5095",true);
                client.ExecuteRemoteCommand("am start -a android.intent.action.VIEW -e mode 1 net.nickac.buttondeck/.MainActivity",device, null);

                    Thread.Sleep(1200);
                }
  //monitor.DeviceConnected += OnDeviceConnected;
               // monitor.Start();
                ClientThread = new ClientThread();
                   ClientThread.Start();
                Application.Run(new MainForm());

  ClientThread.Stop();

                //     Application.Run(new MainForm());



            }
                OBSUtils.Disconnect();

                
              
                NetworkChange.NetworkAddressChanged -= NetworkChange_NetworkAddressChanged;
                NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAddressChanged;
                ApplicationSettingsManager.SaveSettings();
                DevicePersistManager.SaveDevices();
                Trace.Flush();
        }
        public static void OnDeviceConnected(object sender, DeviceDataEventArgs e)
        {
            Console.WriteLine($"The device {e.Device.Name} has connected to this PC");
          //  var client = new AdbClient(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort), Factories.AdbSocketFactory);
        //    client.ExecuteRemoteCommand("usb", e.Device, null);
         //   client.CreateForward(e.Device, 5095, 5095);
  //          client.ExecuteRemoteCommand("am start -a android.intent.action.VIEW -e mode 1 net.nickac.buttondeck/.MainActivity", e.Device, null);

          //  am start -S - D net.nickac.buttondeck / android.app.MainActivity--es name MYNAME--es email test @gmail.com
           //   ClientThread = new ClientThread();
         //   ClientThread.Start();
            Console.WriteLine("STARTING SERVER ON SMARTPHONE...");

        }
        private static void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            if(Program.mode ==0)
            {
   ServerThread.Stop();
            ServerThread = new ServerThread();
            ServerThread.Start();

            }
            else
            {
                ClientThread.Stop();
                ClientThread = new ClientThread();
                ClientThread.Start();

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
    }
}