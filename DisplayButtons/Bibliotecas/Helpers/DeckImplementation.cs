using BackendAPI;
using BackendAPI.Networking;
using BackendAPI.Networking.Implementation;
using BackendAPI.Networking.TcpLib;
using BackendAPI.Utils;
using DisplayButtons;
using Misc;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;
using static BackendAPI.Utils.DevicePersistManager;

namespace DisplayButtons.Helpers
{

 
    public class UsbMode
    {
        public UsbMode()
        {


        }
        public void MountUsbDevices()
        {

            
       

       
            try
            {
                foreach (var device in Program.client.GetDevices().ToList())
                {
                    DevicePersistManager.PersistedDevices.Any(c =>
                    {

                        Debug.WriteLine("TENTANDO COM " + device.State);
                        if (device != null)
                        {

                            c.DeviceUsb = device;
                            Debug.WriteLine("ASSIMILANDO DEVICE AO USB.");
                        }


                        return true;
                    });
                
                    //if (device.Model == "")
                    //{
                    //    Program.client.ExecuteRemoteCommand("getprop ro.product.name", device, product_name);
                    //    Program.client.ExecuteRemoteCommand("getprop ro.product.manufacturer", device, product_manufacter);

                    //    Debug.WriteLine("alterando nome não reconhecido para : " + product_name);

                    //    device.Model = product_name.ToString().TrimEnd(new char[] { '\r', '\n' }); ;
                    //    device.Product = product_manufacter.ToString().TrimEnd(new char[] { '\r', '\n' }); ;
                    //}
                    //Debug.WriteLine("adicionando " + device.Model);





                }

            }
            catch (Exception)
            {

            }



        }
   

    
        public static bool isRetryConnected = false;
        public static bool isConnected = false;


      
     
      
       
            public static bool AlreadyCalled = false;
        
        public static void ConnectedSucessfull()
        {
            //  AlreadyCalled = false;
            aTimer.Enabled = false;
            AlreadyCalled = false;
          
        }
       public static System.Timers.Timer aTimer = new System.Timers.Timer();
        public  static void RetryConnect()
        {


            if (AlreadyCalled) {
                return;
            }
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
            AlreadyCalled = true;

        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)

        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Run));

        }

            public static void Run(object value)
            {
                try
                {

            
                // Do a task
                Program.client.RemoveAllForwards(DevicePersistManager.DeviceUsb);
                Thread.Sleep(500);
                Program.client.CreateForward(DevicePersistManager.DeviceUsb, $"tcp:{ApplicationSettingsManager.Settings.PORT}", $"tcp:{ApplicationSettingsManager.Settings.PORT}", true);

                Initilizator.ClientThread.Stop();
                Initilizator.ClientThread = new Misc.ClientThread();
                Initilizator.ClientThread.Start();
          
                }
                catch(Exception )
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
        public void RefreshCurrentUsb()
        {
            Thread th = new Thread(AutoConnectedUsb);
            th.Start();
        }

               public int CurrentConnections {
            get {
                if(Initilizator.mode == 0)
                {
                 
                    return Initilizator.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Select(m => m.ConnectionGuid).Count(DevicePersistManager.IsDeviceConnected) ?? 0;

                }
                else 
                {
            return Initilizator.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Select(m => m.ConnectionGuid).Count(DevicePersistManager.IsDeviceConnected) ?? 0;


                }
            }
        }
        private void AutoConnectedUsb()
        {


        
                try
                {


                    // PersistUsbMode(DevicePersistManager.DeviceUsb);
                    Program.client.RemoveAllForwards(DevicePersistManager.DeviceUsb);
                    Program.client.CreateForward(DevicePersistManager.DeviceUsb, "tcp:5095", "tcp:5095", true);

                Initilizator.ClientThread.Stop();
                Initilizator.ClientThread = new Misc.ClientThread();
                Initilizator.ClientThread.Start();
                

                        }



                        


                      

                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                  Thread.Sleep(1000);
        
        }
        

    }
    [Serializable]
    public abstract class AbstractDeckInformation
        {
        public  string GetName { get; set; }
            public static Type FindType(string fullName)
            {
                return
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => !a.IsDynamic)
                        .SelectMany(a => a.GetTypes())
                        .FirstOrDefault(t => t.FullName.Equals(fullName));
            }

            public virtual string GetActionName()
        {

            return GetName;
        }
            public abstract string Color();

            public abstract int Size();
            public virtual bool IsPlugin()
            {

                return false;
            }
  
        }

    }
