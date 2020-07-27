using ButtonDeck.Backend.Networking;
using ButtonDeck.Backend.Networking.Implementation;
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
            var list = Program.device_list;

            if (list.Count == 1)
                {



                    Program.client.RemoveAllForwards(Program.client.GetDevices().First());
                    Program.client.CreateForward(Program.client.GetDevices().First(), "tcp:5095", "tcp:5095", true);
                try
                {
  Program.ClientThread.Stop();
                Program.ClientThread = new Misc.ClientThread();
                Program.ClientThread.Start();
                }catch(Exception eee)
                {

                }
              
            }
               
              
            foreach (var item in DevicePersistManager.PersistedDevices.ToList())
            {
            List<Guid> toRemove = new List<Guid>();
             



                try
                {
                   
                 

                      

                            Debug.WriteLine("Device desconectada:" + item.DeviceName + " STATUS USB: " + item.DeviceUsb.State);

                    if (DevicePersistManager.IsDeviceConnected(item.DeviceGuid))
                    {

                        MainForm.Instance.Invoke(new Action(() =>
                            {

                               
                                    Debug.WriteLine("Reconectado.");


                                  MainForm.Instance.StartUsbMode();


                         

                                          

                                     
                             MainForm.Instance.CurrentDevice = item;
                                               //   teste.MountUsbDevices();




                            





                            }));


                    }

                    //  toRemove.Add(item.DeviceGuid);


                }
                catch
                {


                }


          
           // toRemove.All(c => { DevicePersistManager.RemoveConnectionState(c); return true; });
  }
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
