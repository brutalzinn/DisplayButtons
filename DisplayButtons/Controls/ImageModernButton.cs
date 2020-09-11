using DisplayButtons.Forms;
using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.TcpLib;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Utils;
using NickAc.ModernUIDoneRight.Controls;
using NickAc.ModernUIDoneRight.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Bibliotecas.DeckText;
using System.Diagnostics;

namespace DisplayButtons.Controls
{
    public class ImageModernButton : ModernButton
    {

        public int CurrentSlot {
            get {
                try {
                    return int.Parse(ExtractNumber(Name));
                } catch (Exception) {
                    return -1;
                }
            }
        }
        public ImageModernButton Origin { get; set; }


        public static Guid GetConnectionGuidFromDeckDevice(DeckDevice device)
        {
            if (Program.mode == 0)
            {

                var connections = Program.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;

            }
            else
            {
                var connections = Program.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;

            }

        }

        private TextLabel _textlabel;
        public TextLabel TextButton
        {
            get => Origin?.TextButton ?? _textlabel;
            set
            {
                if (Origin != null)
                {
                    Origin.TextButton = value;
                    return;
                }
                _textlabel = value;
                Refresh();


            }
        }
        public string ExtractNumber(string original)
        {
            return new string(original.Where(Char.IsDigit).ToArray());
        }

        private Image _image;
      
        public Image NormalImage {
            get => Origin?._image ?? _image; set {
                if (Origin != null) {
                    Origin._image = value;
                    return;
                }
                _image = value;
                if (IsHandleCreated)
                    Invoke(new Action(Refresh));
            }
        }

        public new Image Image {
            get => Origin?.Image ?? _image;
            set {
                if (Origin != null) {
                    Origin.Image = value;
                    return;
                }
                _image = value;
                Refresh();

                if (Parent != null && Parent.Parent != null && Parent.Parent is MainForm frm) {
                    if (frm.CurrentDevice != null) {
                        int slot = int.Parse(ExtractNumber(Name));
                        IEnumerable<ConnectionState> connections;
                        if (Program.mode == 0)
                        {

                         connections = Program.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());


                        }
                        else
                        {
                             connections = Program.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());


                        }
                        var stateID = GetConnectionGuidFromDeckDevice(frm.CurrentDevice);
                        var state = connections.FirstOrDefault(m => m.ConnectionGuid == stateID);
                        if (value == null) {
                            //Send clear packet
                            state?.SendPacket(new SlotImageClearPacket(slot));
                            return;
                        }

                        Bitmap bmp = new Bitmap(value);
                        var deckImage = new DeckImage(bmp);
                        
                        if (Tag is DynamicDeckItem itemTag) {
                            itemTag.DeckImage = deckImage;

                        } else if (Tag is DynamicDeckFolder itemFolder) {
                            itemFolder.DeckImage = deckImage;
                        }
                        if (Tag is IDeckItem itemNew)
                        {
                            if (state != null)
                            {
                                state.SendPacket(new SingleUniversalChangePacket(deckImage)
                                {
                                    ImageSlot = slot,
                                    CurrentItem = itemNew

                                }); 
                            }
                        }
                        if (Tag is DynamicDeckItem item) {
                            var device = frm.CurrentDevice;
                            device.CheckCurrentFolder();
                            device.CurrentProfile.Currentfolder.Add(slot, item);
                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);





            if (Image != null)
            {
                if (TextButton != null)
                {
                    using (Font font1 = new Font("Arial", TextButton.Size, FontStyle.Bold, GraphicsUnit.Point))
                    {
                        Graphics g = this.CreateGraphics();

                        SizeF size = g.MeasureString(TextButton.Text,font1);
                        StringFormat format = new StringFormat();
                     
                        int nLeft = Convert.ToInt32((this.ClientRectangle.Width / 2) - (size.Width / 2));
                        int nTop = Convert.ToInt32(TextHelper.PercentOf(TextButton.Position,this.Size.Height));
              



                        RectangleF rectF1 = new RectangleF(nLeft, nTop, Image.Width, Image.Height);
                        Debug.WriteLine("top result: " + nTop);
                        Debug.WriteLine("TextButton.Position: " + TextButton.Position);
                        pevent.Graphics.DrawImage(Image, DisplayRectangle);

                        pevent.Graphics.DrawString(TextButton.Text, font1, TextButton.Brush, rectF1);

                    }
                }
                else
                {
                    pevent.Graphics.DrawImage(Image, DisplayRectangle);

                }


            }
        }
        

    }
}
