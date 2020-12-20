using BackendAPI.Networking.Attributes;
using BackendAPI.Networking.IO;
using BackendAPI.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
namespace BackendAPI.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SlotImageChangeChunkPacket : INetworkPacket
    {
        IDictionary<int, DeckImage> toSend = new Dictionary<int, DeckImage>();

        public void AddToQueue(int slot, DeckImage img)
        {
            toSend.Add(slot, img);
        }

        public override void FromInputStream(DataInputStream reader)
        {
        }

        public override long GetPacketNumber() => 7;

        public override void ToOutputStream(DataOutputStream writer)
        {


            //The number of images to send
           // writer.WriteInt(toSend.Count);
           // foreach (var item in toSend) {
             //   SendDeckImage(writer, item.Key, item.Value);
           // }

        }

        private void SendDeckImage(DataOutputStream writer, int slot, DeckImage img)
        {
            if (img != null) {
                //Write the slot
                writer.WriteInt(slot);
                //Write the byte array lenght
                writer.WriteInt(img.InternalBitmap.Length);
                //Write the byte array
                writer.Write(img.InternalBitmap);
        

                string base64Image = img.ImageToBase64(img.Bitmap,System.Drawing.Imaging.ImageFormat.Jpeg);
                using (WebClient client = new WebClient())
                {
                    byte[] response = client.UploadValues("http://127.0.0.1", new NameValueCollection()
                {
                    { "myImageData", base64Image }
                });


                }

            }
        }

        public override object Clone()
        {
            return new SlotImageChangeChunkPacket();
        }
      
    }
}
