using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Objects;
using Microsoft.VisualC.StlClr;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ButtonDeck.Bibliotecas.CustomButtonJsonsModel;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class SlotUniversalChangeChunkPacket : INetworkPacket
    {
 
        
  
        List<Labels> list_labels = new List<Labels>();
        public void AddToQueue(int slot, string text, string font,int size, int position, string color, DeckImage image)
        {
         
            list_labels.Add (new Labels( slot, font, size, position,text, color, image));
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
              SendDeckLabel(writer, item.Id, item.Font,item.Size,item.Position,item.Text,item.Color, item.Image) ;

             }

        }

        private void SendDeckLabel(DataOutputStream writer, int slot, string font,int size,int pos,string text,string color , DeckImage img)
        {

            //Write the slot
            if (img != null)
            {
                Json headerContent = new Json();

     
                writer.WriteInt(slot);
                //Byte array lenght
                writer.WriteInt(img.InternalBitmap.Length);
                writer.Write(img.InternalBitmap);

                headerContent.Font = font;
                headerContent.Size = size;
                headerContent.Position = pos;
                headerContent.Text = text;
                headerContent.Color = color;
                


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
