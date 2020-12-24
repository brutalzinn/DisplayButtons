using BackendAPI.Networking.TcpLib;
using BackendAPI.Utils;
using NickAc.ModernUIDoneRight.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using SharpAdbClient;
using DisplayButtons.Forms;
using DisplayButtons;
using BackendAPI;

namespace Misc
{
    public class DevicesTitlebarButton : NickAc.ModernUIDoneRight.Objects.Interaction.ModernTitlebarButton
    {
        private readonly MainForm _frm;


        public DevicesTitlebarButton(MainForm frm)
        {
            _frm = frm;
            Click += DevicesTitlebarButton_Click;
        }

        private void DevicesTitlebarButton_Click(object sender, MouseEventArgs e)
        {

           RefreshCurrentDevices();

            int controlSize = _frm.TitlebarHeight * 2;
            System.Drawing.Size result;
            if (Initilizator.mode == 0)
            {
         result = new Size(Width * 2, Math.Max(controlSize * CurrentConnections, controlSize) + 2);

            }
            else
            {

                var list = DisplayButtons.Program.device_list;
                result = new Size(Width * 2, Math.Max(controlSize * list.Count, controlSize) + 2);

            }

            ModernForm frm = new ModernForm()
            {
                Sizable = false,
                ShowInTaskbar = false,
                BackColor = _frm.BackColor,
                ForeColor = _frm.ColorScheme.ForegroundColor,
                ColorScheme = _frm.ColorScheme,
                TitlebarVisible = false,
                MinimumSize = new Size(0, controlSize),

                Size = result
        };

            //Hacky method to get this button rectangle on screen
            var rect = _frm.RectangleToScreen(_frm.TitlebarButtonsRectangle);
            rect.Width = rect.Width - (rect.Width - Width);
            frm.Location = new Point(rect.Right - frm.Width, rect.Bottom);
            frm.Deactivate += (s, ee) => frm.Dispose();
            Size controlFinalSize = new Size(frm.DisplayRectangle.Width, controlSize);
            if (DisplayButtons.Program.mode == 0)
            {
             
                //Load devices
                foreach (var device in DevicePersistManager.PersistedDevices)
                {
                    try
                    {

                        var ctrl = new DeckDeviceInformationControl()
                        {
                            DeckDevice = device,
                            Size = controlFinalSize,
                            ForeColor = _frm.ColorScheme.SecondaryColor,
                            Dock = DockStyle.Top,
                            Tag = _frm
                        };

                        frm.Controls.Add(ctrl);


                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

          
            }
            else
            {


             
            
                //  var devices = AdbClient.Instance.GetDevices();
             
                foreach (DeviceData device in DisplayButtons.Program.device_list)
                {
                    
                   
                  //  Debug.WriteLine("adicionando " + device.Model);
                    try
                    {
                        var ttt = new DeckDeviceInformationControl()
                        {

                           
                            DeckUsb = device,
                            Size = controlFinalSize,
                            ForeColor = _frm.ColorScheme.SecondaryColor,
                            Dock = DockStyle.Top,
                            Tag = _frm
                        };
                        frm.Controls.Add(ttt);
                    }
                    catch (Exception ee)
                    {
                        Debug.WriteLine(ee);
                       continue;
                    }




                }


            
            }

              frm.Show();
        }
       
    public void RefreshCurrentDevices()
        {
            Thread th = new Thread(UpdateConnectedDevices);
            th.Start();
        }
        public void RefreshCurrentUsb()
        {
            Thread th = new Thread(AutoConnectedUsb);
            th.Start();
        }
     



        public int CurrentConnections {
       
            get {

                int result = 0;
                try
                {
                    if (DisplayButtons.Program.mode == 0)
                    {

                         result = Initilizator.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Select(m => m.ConnectionGuid).Count(DevicePersistManager.IsDeviceConnected) ?? 0;

                    }
                    else
                    {

                        result = Initilizator.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Select(m => m.ConnectionGuid).Count(DevicePersistManager.IsDeviceConnected) ?? 0;


                    }
                }
                catch (Exception)
                {

                }
                return result;
            }
        }
        public override string Text {
            get {
                _frm.ChangeButtonsVisibility(DevicePersistManager.IsVirtualDeviceConnected);
         
                return $"{CurrentConnections} "+ Texts.rm.GetString("CONNECTEDDEVICES", Texts.cultereinfo) + $"{(CurrentConnections != 1 ? "s" : "")}";
            }
            set => base.Text = value;
        }



private void UpdateConnectedDevices()
        {

            if (DisplayButtons.Program.mode == 0)
            {


                List<Guid> toRemove = new List<Guid>();
                DevicePersistManager.DeckDevicesFromConnection.All(c =>
                {
                    if (!Initilizator.ServerThread.TcpServer.Connections.OfType<ConnectionState>().Any(d => d.ConnectionGuid == c.Key))
                    {
                        toRemove.Add(c.Key);
                    }
                    return true;
                });
                toRemove.All(c => { DevicePersistManager.RemoveConnectionState(c); return true; });

            }
            else
            {


                List<Guid> toRemove = new List<Guid>();
                DevicePersistManager.DeckDevicesFromConnection.All(c =>
                {
                    if (!Initilizator.ClientThread.TcpClient.Connections.OfType<ConnectionState>().Any(d => d.ConnectionGuid == c.Key))
                    {
                        toRemove.Add(c.Key);
                    }
                    return true;
                });
                toRemove.All(c => { DevicePersistManager.RemoveConnectionState(c); return true; });









            }
        }
        private void AutoConnectedUsb()
        {
            List<Guid> toRemove = new List<Guid>();

            foreach(var item in DevicePersistManager.PersistedDevices.ToList())
            {
                

             
                if (!Initilizator.ClientThread.TcpClient.Connections.OfType<ConnectionState>().Any(d => d.ConnectionGuid == item.DeviceGuid))
                {
                    
                    if (item.GetConnection() != null)
                    {

                    }
                    else
                    {

                       
                        if (item.DeviceUsb != null)
                        {
                            
                                Debug.WriteLine("Device desconectada:" + item.DeviceName + " STATUS USB: " + item.DeviceUsb.State);
                            DisplayButtons.Program.client.RemoveAllForwards(item.DeviceUsb);
                            DisplayButtons.Program.client.CreateForward(item.DeviceUsb, "tcp:5095", "tcp:5095", true);
                            //  Program.client.ExecuteRemoteCommand("am force-stop net.nickac.DisplayButtons", item.DeviceUsb, null);
                            //     Thread.Sleep(1400);
                            //      Program.client.ExecuteRemoteCommand("am start -a android.intent.action.VIEW -e mode 1 net.nickac.DisplayButtons/.MainActivity", item.DeviceUsb, null);
                            //        Thread.Sleep(1200);
                            Initilizator.ClientThread.Stop();
                            Initilizator.ClientThread = new Misc.ClientThread();
                            Initilizator.ClientThread.Start();
                                MainForm.Instance.Invoke(new Action(() =>
                                {
                                    if(item.GetConnection() != null)
                                    {
                                     MainForm.Instance.StartLoad(false);
                                    MainForm.Instance.Start_configs();
                                    }
                                   
                                }));
                            
                        }
                    }

                    toRemove.Add(item.DeviceGuid);

                }
                
               
              
            }
            toRemove.All(c => { DevicePersistManager.RemoveConnectionState(c); return true; });

        }
        public override int Width { get => TextRenderer.MeasureText(Text, Font).Width + 16; set => base.Width = value; }

    }
}
