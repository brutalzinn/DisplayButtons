using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Utils;
using ButtonDeck.Misc;
using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Utils;
using NickAc.ModernUIDoneRight.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ButtonDeck.Backend.Utils.DevicePersistManager;
using SharpAdbClient;
using System.Net;
using System.Threading;

namespace ButtonDeck.Forms
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
                        if (device_usb_model != null)
                        {


                            TextBox txtBox = new TextBox()
                            {
                                Bounds = NameLabelRectangleWithoutPrefix,
                                Width = Width - Padding.Right * 2,
                                Text = device_usb_model,
                                BorderStyle = BorderStyle.None,
                                BackColor = BackColor,
                                ForeColor = ForeColor,
                            };
                            txtBox.LostFocus += (s, ee) =>
                            {
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    device_usb_model = txtBox.Text.Trim();
                                    Refresh();
                                }
                                Controls.Remove(txtBox);
                            };
                            txtBox.KeyUp += (s, ee) =>
                            {
                                if (ee.KeyCode != Keys.Enter) return;
                                if (txtBox.Text.Trim() != string.Empty)
                                {
                                    device_usb_model = txtBox.Text.Trim();
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
                            if (IsVirtualDeviceConnected)
                            {
                                Program.client.RemoveAllForwards(DeviceUsb);
                                Program.client.CreateForward(DeviceUsb, "tcp:5095", "tcp:5095", true);
                                Program.client.ExecuteRemoteCommand("am force-stop net.nickac.buttondeck", DeviceUsb, null);
                                Thread.Sleep(1400);
                                Program.client.ExecuteRemoteCommand("am start -a android.intent.action.VIEW -e mode 1 net.nickac.buttondeck/.MainActivity", DeviceUsb, null);
                                Thread.Sleep(1200);
                                Program.ClientThread.Stop();
                                Program.ClientThread = new Misc.ClientThread();
                                Program.ClientThread.Start();

                                MainForm.Instance.StartLoad();
                                MainForm.Instance.Start_configs();
                            }
                        }
                    }
                }

                if (DeckDevice != null) {
                    if (NameLabelRectangle.Contains(e2.Location)) {
                        TextBox txtBox = new TextBox()
                        {
                            Bounds = NameLabelRectangleWithoutPrefix,
                            Width = Width - Padding.Right * 2,
                            Text = DeckDevice.DeviceName,
                            BorderStyle = BorderStyle.None,
                            BackColor = BackColor,
                            ForeColor = ForeColor,
                        };
                        txtBox.LostFocus += (s, ee) => {
                            if (txtBox.Text.Trim() != string.Empty) {
                                DeckDevice.DeviceName = txtBox.Text.Trim();
                                Refresh();
                            }
                            Controls.Remove(txtBox);
                        };
                        txtBox.KeyUp += (s, ee) => {
                            if (ee.KeyCode != Keys.Enter) return;
                            if (txtBox.Text.Trim() != string.Empty) {
                                DeckDevice.DeviceName = txtBox.Text.Trim();
                                Refresh();
                            }
                            Controls.Remove(txtBox);
                        };
                        Controls.Add(txtBox);
                        txtBox.Focus();
                    } else {
                        if (Tag is MainForm frm) {
                            if (IsVirtualDeviceConnected) {

                              
                           
                                Debug.WriteLine("CHEGOU 2");
                                if (frm.CurrentDevice.DeviceGuid == DeckDevice.DeviceGuid) {
                                    //Someone clicked on the same device. Unload this one.
                                    OnDeviceDisconnected(this, DeckDevice);
                                    IsVirtualDeviceConnected = false;


                                    frm.ChangeButtonsVisibility(false);
                                    frm.CurrentDevice = null;
                                    frm.RefreshAllButtons(false);
                                    frm.Activate();
                                } else {
                                    //Someone requested another device. Unload current virtual device..
                                    OnDeviceDisconnected(this, frm.CurrentDevice);
                                    IsVirtualDeviceConnected = false;
                                    frm.ChangeButtonsVisibility(false);
                                    frm.CurrentDevice = null;
                                    frm.RefreshAllButtons(false);
                                }
                            } else {
                            
                                Debug.WriteLine("CHEGOU 3");
                                frm.CurrentDevice = DeckDevice;
                                IsVirtualDeviceConnected = true;
                                OnDeviceConnected(this, DeckDevice);
                                frm.ChangeButtonsVisibility(true);
                                frm.RefreshAllButtons(false);
                                void tempConnected(object s, DeviceEventArgs ee)
                                {
                                    if (ee.Device.DeviceGuid == DeckDevice.DeviceGuid) return;
                                    DeviceConnected -= tempConnected;
                                    if (IsVirtualDeviceConnected) {
                                        //We had a virtual device.
                                        OnDeviceDisconnected(this, DeckDevice);
                                        IsVirtualDeviceConnected = false;
                                        frm.ChangeButtonsVisibility(false);
                                    }
                                }
                                DeviceConnected += tempConnected;
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
        public string device_usb_model { get; set; }
        public string device_usb_manufacturer { get; set; }
        public DeviceData DeviceUsb { get; set; }

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

                    if (DeviceUsb == null) return Rectangle.Empty;
                    int textHeight = (int)TextRenderer.MeasureText("AaBbCc", Font).Height;
                    return new Rectangle(Padding.Left, Padding.Top, (int)MeasureString((DeviceUsb.Product + DeviceUsb.Name), Font).Width, textHeight);


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

                    if (DeviceUsb == null) return Rectangle.Empty;
                    Rectangle rect = NameLabelRectangle;
                    return Rectangle.FromLTRB((int)(rect.Left + MeasureString(DeviceUsb.Product, Font).Width), rect.Top, rect.Right, rect.Bottom);

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
                if (DeviceUsb == null) return;
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
                    e.Graphics.DrawString(DeviceUsb.Product + " " +  DeviceUsb.Model.Replace("_"," "), Font, sb, Padding.Left, Padding.Top);

                    using (var sb2 = new SolidBrush(Color.FromArgb(150, ForeColor)))
                    {
                        string status = "";

                        foreach (var item in DevicePersistManager.DeckDevicesFromConnection)
                        {


                            var teste = item.Value.GetConnection();
                            if (teste != null)
                            {

                                status = "Sincronizado";
                            }
                           
                        }
                        if (status == "")
                        {

                           status= "Sem sincronia";
                        }
                        e.Graphics.DrawString("MODO USB | " + DeviceUsb.State + " | " + status, Font, sb, Padding.Left, Padding.Top + textHeight); ;
                    }
                }
                }

        }
    }
}
