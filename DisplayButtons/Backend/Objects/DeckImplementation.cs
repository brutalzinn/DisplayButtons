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

            //System.Threading.SpinWait.SpinUntil(() => Globals.can_refresh);

            try {
               


               
                    Program.client.RemoveAllForwards(DevicePersistManager.DeviceUsb);
                    Thread.Sleep(1000);
                    Program.client.CreateForward(DevicePersistManager.DeviceUsb, "tcp:5095", "tcp:5095", true);
                Program.ClientThread.Stop();
                    Program.ClientThread = new Misc.ClientThread();
                    Program.ClientThread.Start();

                foreach (var item in DevicePersistManager.PersistedDevices.ToList())
                {

                    if (DevicePersistManager.IsDeviceOnline(item))
                    {

                        MainForm.Instance.Invoke(new Action(() =>
                            {

                                Debug.WriteLine("Reconectado.");
                                MainForm.Instance.StartUsbMode();
                                DevicePersistManager.ChangeConnectedState(item.GetConnection(), item);
                                MainForm.Instance.CurrentDevice.CurrentProfile = MainForm.Instance.CurrentPerfil.Value;
                                MainForm.Instance.CurrentDevice = item;
                              
                                
                                //  DevicePersistManager.OnDeviceConnected(this, item);
                                // MainForm.Instance.Start_configs();
                                //MainForm.Instance.CurrentDevice.CurrentProfile = MainForm.Instance.CurrentPerfil.Value;
                                // MainForm.Instance.CurrentDevice = item;




                            }));

                    }
                    }





                }




                catch (Exception eee)
            {
                Debug.WriteLine(eee);

            }
            // toRemove.All(c => { DevicePersistManager.RemoveConnectionState(c); return true; });

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
