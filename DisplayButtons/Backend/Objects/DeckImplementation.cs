using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Networking.TcpLib;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Forms;
using DisplayButtons.Misc;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static DisplayButtons.Backend.Utils.DevicePersistManager;

namespace DisplayButtons.Backend.Objects
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
        int _count;

      public  void ButtonClick(object sender, EventArgs e)
        {
            ThreadWorker worker = new ThreadWorker();
            worker.ThreadDone += HandleThreadDone;

            Thread thread1 = new Thread(worker.Run);
            thread1.Start();

            _count = 1;
        }
        public void HandleThreadDone(object sender, EventArgs e)
        {
            // You should get the idea this is just an example

            
            
            
        }
      public  static AutoResetEvent _AREvt;

       
        public class ThreadWorker
        {
            public event EventHandler ThreadDone;

            public void Run()
            {
               
                // Do a task
                Program.client.RemoveAllForwards(DevicePersistManager.DeviceUsb);
                Program.client.CreateForward(DevicePersistManager.DeviceUsb, $"tcp:{ApplicationSettingsManager.Settings.PORT}", $"tcp:{ApplicationSettingsManager.Settings.PORT}", true);

                Program.ClientThread.Stop();
                Program.ClientThread = new Misc.ClientThread();
                Program.ClientThread.Start();
                _AREvt.WaitOne(10, true);
                if (ThreadDone != null)
                    ThreadDone(this, EventArgs.Empty);
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
                if(Program.mode == 0)
                {
                 
                    return Program.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Select(m => m.ConnectionGuid).Count(DevicePersistManager.IsDeviceConnected) ?? 0;

                }
                else 
                {
            return Program.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Select(m => m.ConnectionGuid).Count(DevicePersistManager.IsDeviceConnected) ?? 0;


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
  
                    Program.ClientThread.Stop();
                    Program.ClientThread = new Misc.ClientThread();
                    Program.ClientThread.Start();
                

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
