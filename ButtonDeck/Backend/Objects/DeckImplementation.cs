using ButtonDeck.Backend.Networking.TcpLib;
using ButtonDeck.Backend.Utils;
using ButtonDeck.Forms;
using ButtonDeck.Misc;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects
{

 
    public class UsbMode
    {
        public void MountUsbDevices()
        {




            var product_name = new ConsoleOutputReceiver();
            var product_manufacter = new ConsoleOutputReceiver();
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
                    if (device.Model == "")
                    {
                        Program.client.ExecuteRemoteCommand("getprop ro.product.name", device, product_name);
                        Program.client.ExecuteRemoteCommand("getprop ro.product.manufacturer", device, product_manufacter);

                        Debug.WriteLine("alterando nome não reconhecido para : " + product_name);

                        device.Model = product_name.ToString().TrimEnd(new char[] { '\r', '\n' }); ;
                        device.Product = product_manufacter.ToString().TrimEnd(new char[] { '\r', '\n' }); ;
                    }
                    Debug.WriteLine("adicionando " + device.Model);





                }

            }
            catch (Exception)
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



                var result = Program.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Select(m => m.ConnectionGuid).Count(DevicePersistManager.IsDeviceConnected);
                if (result <= 1)
                {
                
                    Program.client.CreateForward(Program.client.GetDevices().First(), "tcp:5095", "tcp:5095", true);
                    Program.ClientThread.Stop();
                    Program.ClientThread = new Misc.ClientThread();
                    Program.ClientThread.Start();
                    Thread.Sleep(3000);
                }
                


            }
            catch { }



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
