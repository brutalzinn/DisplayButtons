﻿
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using SharpAdbClient;
using System.Threading;
using BackendAPI.Utils;
using BackendAPI;
using BackendAPI.Objects;
using Misc;

namespace DisplayButtons.Forms
{
    class DeckDeviceInformationControl : Control
    {
        private SizeF MeasureString(string text, Font font)
        {
            using (Bitmap bmp = new Bitmap(1, 1)) {
                using (Graphics g = Graphics.FromImage(bmp)) {
                    return g.MeasureString(text, font);
                }
            }
        }


        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (e is MouseEventArgs e2) {

                if (Program.mode == 1)
                {

                    if (NameLabelRectangle.Contains(e2.Location))
                    {
                        if (DeckUsb != null)
                        {


                            TextBox txtBox = new TextBox()
                            {
                                Bounds = NameLabelRectangleWithoutPrefix,
                                Width = Width - Padding.Right * 2,
                                Text = DeckUsb.Model,
                                BorderStyle = BorderStyle.None,
                                BackColor = BackColor,
                                ForeColor = ForeColor,
                            };
                            txtBox.LostFocus += (s, ee) =>
                            {
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    DeckUsb.Model = txtBox.Text.Trim();
                                    Refresh();
                                }
                                Controls.Remove(txtBox);
                            };
                            txtBox.KeyUp += (s, ee) =>
                            {
                                if (ee.KeyCode != Keys.Enter) return;
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    DeckUsb.Model = txtBox.Text.Trim();
                                    Refresh();
                                }
                                Controls.Remove(txtBox);
                            };
                            Controls.Add(txtBox);
                            txtBox.Focus();
                        }
                    }
                    else
                    {
                        if (Tag is MainForm frm)
                        {
                            if (DevicePersistManager.IsDeviceTest)
                            {
                                Program.client.RemoveAllForwards(DeckUsb);
                                Program.client.CreateForward(DevicePersistManager.DeviceUsb, $"tcp:{ApplicationSettingsManager.Settings.PORT}", $"tcp:{ApplicationSettingsManager.Settings.PORT}", true);

                                Program.client.ExecuteRemoteCommand("am force-stop net.nickac.DisplayButtons", DeckUsb, null);
                                Thread.Sleep(1400);
                                Program.client.ExecuteRemoteCommand("am start -a android.intent.action.VIEW -e mode 1 net.nickac.DisplayButtons/.MainActivity", DeckUsb, null);
                                Thread.Sleep(1200);
                                Initilizator.ClientThread.Stop();
                                Initilizator.ClientThread = new Misc.ClientThread();
                                Initilizator.ClientThread.Start();
                             //   DevicePersistManager.PersistUsbMode(DeckUsb);
                         //       MainForm.Instance.StartLoad(true);
                             //   MainForm.Instance.Start_configs();
                            }
                            else
                            {
                              //  PersistUsbMode(DeckUsb);
                                Program.client.RemoveAllForwards(DeckUsb);
                                Program.client.CreateForward(DevicePersistManager.DeviceUsb, $"tcp:{ApplicationSettingsManager.Settings.PORT}", $"tcp:{ApplicationSettingsManager.Settings.PORT}", true);

                                Initilizator.ClientThread.Stop();
                                Initilizator.ClientThread = new Misc.ClientThread();
                                Initilizator.ClientThread.Start();




                            }
                        }
                    }
                }
                else
                {



                    if (DeckDevice != null)
                    {
                        if (NameLabelRectangle.Contains(e2.Location))
                        {
                            TextBox txtBox = new TextBox()
                            {
                                Bounds = NameLabelRectangleWithoutPrefix,
                                Width = Width - Padding.Right * 2,
                                Text = DeckDevice.DeviceName,
                                BorderStyle = BorderStyle.None,
                                BackColor = BackColor,
                                ForeColor = ForeColor,
                            };
                            txtBox.LostFocus += (s, ee) =>
                            {
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    DeckDevice.DeviceName = txtBox.Text.Trim();
                                    Refresh();
                                }
                                Controls.Remove(txtBox);
                            };
                            txtBox.KeyUp += (s, ee) =>
                            {
                                if (ee.KeyCode != Keys.Enter) return;
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    DeckDevice.DeviceName = txtBox.Text.Trim();
                                    Refresh();
                                }
                                Controls.Remove(txtBox);
                            };
                            Controls.Add(txtBox);
                            txtBox.Focus();
                        }
                        else
                        {
                            if (Tag is MainForm frm)
                            {
                                if (DevicePersistManager.IsVirtualDeviceConnected)
                                {



                                    Debug.WriteLine("DEVICE MNANAWE 2");
                                    if (frm.CurrentDevice.DeviceGuid == DeckDevice.DeviceGuid)
                                    {
                                        //Someone clicked on the same device. Unload this one.
                                        DevicePersistManager.OnDeviceDisconnected(this, DeckDevice);
                                        DevicePersistManager.IsVirtualDeviceConnected = false;


                                        frm.ChangeButtonsVisibility(false);
                                        frm.CurrentDevice = null;
                                        frm.RefreshAllButtons(false);
                                        frm.Activate();
                                    }
                                    else
                                    {
                                        Debug.WriteLine("DEVICE MNANAWE 2");
                                        //Someone requested another device. Unload current virtual device..
                                        DevicePersistManager.OnDeviceDisconnected(this, frm.CurrentDevice);
                                        DevicePersistManager.IsVirtualDeviceConnected = false;
                                        frm.ChangeButtonsVisibility(false);
                                        frm.CurrentDevice = null;
                                        frm.RefreshAllButtons(false);
                                    }
                                }
                                else
                                {

                                    Debug.WriteLine("DEVICE MNANAWE 3");
                                    frm.CurrentDevice = DeckDevice;
                                    DevicePersistManager.IsVirtualDeviceConnected = true;
                                    DevicePersistManager.OnDeviceConnected(this, DeckDevice);
                                    frm.ChangeButtonsVisibility(true);
                                    frm.RefreshAllButtons(false);
                                    void tempConnected(object s, DevicePersistManager.DeviceEventArgs ee)
                                    {
                                        if (ee.Device.DeviceGuid == DeckDevice.DeviceGuid) return;
                                        DevicePersistManager.DeviceConnected -= tempConnected;
                                        if (DevicePersistManager.IsVirtualDeviceConnected)
                                        {
                                            //We had a virtual device.
                                            DevicePersistManager.OnDeviceDisconnected(this, DeckDevice);
                                            DevicePersistManager.IsVirtualDeviceConnected = false;
                                            frm.ChangeButtonsVisibility(false);
                                        }
                                    }
                                    DevicePersistManager.DeviceConnected += tempConnected;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Focus();
        }

        public bool Selected {
            get { return _selected; }
            set {
                _selected = value;
                Invalidate();
            }
        }
        const string OFFLINE_PREFIX = "[OFFLINE]";
        public DeckDevice DeckDevice {
            get { return _deckDevice; }
            set {
                _deckDevice = value;
                deviceNamePrefix = (!DevicePersistManager.IsDeviceOnline(DeckDevice) ? $"{OFFLINE_PREFIX} " : "");
            }
        }


        public DeviceData DeckUsb
        {
            get { return _deviceusb; }
            set
            {
                _deviceusb = value;
             
            }
        }

        private DeviceData _deviceusb;

        public new Padding Padding = new Padding(5);
        private bool _selected;
        private DeckDevice _deckDevice;
        private string deviceNamePrefix;
        Stopwatch lastClick = new Stopwatch();



        private static Color GetReadableForeColor(Color c)
        {
            return (((c.R + c.B + c.G) / 3) > 128) ? Color.Black : Color.White;
        }


        public Rectangle NameLabelRectangle {
            get {
                if(Program.mode == 0)
                {


                
                if (DeckDevice == null) return Rectangle.Empty;
                int textHeight = (int)TextRenderer.MeasureText("AaBbCc", Font).Height;
                return new Rectangle(Padding.Left, Padding.Top, (int)MeasureString((deviceNamePrefix + DeckDevice.DeviceName), Font).Width, textHeight);
                }
                else
                {

                    if (DeckUsb == null) return Rectangle.Empty;
                    int textHeight = (int)TextRenderer.MeasureText("AaBbCc", Font).Height;
                    return new Rectangle(Padding.Left, Padding.Top, (int)MeasureString((DeckUsb.Product + DeckUsb.Model), Font).Width, textHeight) ;


                }
            }
        }
        public Rectangle NameLabelRectangleWithoutPrefix {
            get {
                if(Program.mode == 0)
                {
  if (DeckDevice == null) return Rectangle.Empty;
                Rectangle rect = NameLabelRectangle;
                return Rectangle.FromLTRB((int)(rect.Left + MeasureString(deviceNamePrefix, Font).Width), rect.Top, rect.Right, rect.Bottom);




                }
                else
                {

                    if (DeckUsb == null) return 
                            Rectangle.Empty;
                    Rectangle rect = NameLabelRectangle;
                    return Rectangle.FromLTRB((int)(rect.Left + MeasureString(DeckUsb.Product + DeckUsb.Model, Font).Width), rect.Top, rect.Right, rect.Bottom);

                }

            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if(Program.mode == 0) { 
            if (DeckDevice == null) return;
            int textHeight = (int)e.Graphics.MeasureString("AaBbCc", Font).Height;
            var backgroundColor = Selected ? ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme).SecondaryColor : Color.Transparent;
            using (var sb = new SolidBrush(Selected ? GetReadableForeColor(backgroundColor) : ForeColor)) {
                if (Selected) {
                    using (var sb2 = new SolidBrush(backgroundColor)) {
                        e.Graphics.FillRectangle(sb2, new Rectangle(Point.Empty, Size));
                    }
                }
                e.Graphics.DrawString(deviceNamePrefix + DeckDevice.DeviceName, Font, sb, Padding.Left, Padding.Top);
                using (var sb2 = new SolidBrush(Color.FromArgb(150, ForeColor))) {
                    e.Graphics.DrawString("ID: " + DeckDevice.DeviceGuid, Font, sb, Padding.Left, Padding.Top + textHeight);
                }
                }
            }
            else
            {
                if (DeckUsb == null) return;
                int textHeight = (int)e.Graphics.MeasureString("AaBbCc", Font).Height;
                var backgroundColor = Selected ? ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme).SecondaryColor : Color.Transparent;
                using (var sb = new SolidBrush(Selected ? GetReadableForeColor(backgroundColor) : ForeColor))
                {
                    if (Selected)
                    {
                        using (var sb2 = new SolidBrush(backgroundColor))
                        {
                            e.Graphics.FillRectangle(sb2, new Rectangle(Point.Empty, Size));
                        }
                    }
                    e.Graphics.DrawString(DeckUsb.Product + " " +  DeckUsb.Model, Font, sb, Padding.Left, Padding.Top);

                    using (var sb2 = new SolidBrush(Color.FromArgb(150, ForeColor)))
                    {
                        string status;
                        if (DevicePersistManager.IsPersistedUsbMode() == true )
                        {
                            status = "Connected";
                        }
                        else
                        {
                            status = "Disconnected";
                        }
                     e.Graphics.DrawString("MODO: USB" + "Persisted: " + status, Font, sb, Padding.Left, Padding.Top + textHeight);
                    }
                }
                }

        }
    }
}
