using DisplayButtons.Backend.Networking.Attributes;
using DisplayButtons.Backend.Networking.IO;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Bibliotecas;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DisplayButtons.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class SlotUniversalChangeChunkPacket : INetworkPacket
    {
 
        
  
        List<Labels> list_labels = new List<Labels>();
        public void AddToQueue(int slot, IDeckItem item, DeckImage image)
        {
         
            list_labels.Add (new Labels( slot,item, image));
        }
        public void ClearPacket()
        {

            list_labels.Clear();

        }
        public override void FromInputStream(DataInputStream reader)
        {

        }

        public override long GetPacketNumber() => 12;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //The number of images to send
      

       


            //     Debug.WriteLine(jsonString)

            writer.WriteInt(list_labels.Count);
         //   writer.WriteUTF(jsonString);

           foreach (var item in list_labels)
           {
              SendDeckLabel(writer, item.Id, item.Item, item.Image) ;

             }

        }

        private void SendDeckLabel(DataOutputStream writer, int slot, IDeckItem item , DeckImage img)
        {

            //Write the slot
            if (img != null)
            {
                Json headerContent = new Json();

     
                writer.WriteInt(slot);
                //Byte array lenght
                writer.WriteInt(img.InternalBitmap.Length);
                writer.Write(img.InternalBitmap);

                headerContent.Font = " ";
                headerContent.Size = item.Decksize;
                headerContent.Position = item.Deckposition;
                headerContent.Text = item.Deckname;
                headerContent.Color = item.Deckcolor;

                headerContent.Stroke_color = item.Stroke_color;
                headerContent.Stroke_dx = item.Stroke_dxtext;
                headerContent.Stroke_radius = item.Stroke_radius;
                headerContent.Stroke_dy = item.Stroke_Dy;
                headerContent.IsStroke = item.IsStroke;
                headerContent.Isboldtext = item.Isboldtext;
                headerContent.Isnormaltext = item.Isnormaltext;
                headerContent.Isitalictext = item.Isitalictext;
                headerContent.Ishinttext = item.Ishinttext;
                string jsonString = JsonConvert.SerializeObject(headerContent, Formatting.None);

                writer.WriteUTF(jsonString);

                ////text

                //writer.WriteUTF(text);
                ////size
                //writer.WriteInt(size);
                ////position
                //writer.WriteInt(pos);

                ////color

                //writer.WriteUTF(color);
            }
        }

        public override object Clone()
        {
            return new SlotUniversalChangeChunkPacket();
        }
    }
}
