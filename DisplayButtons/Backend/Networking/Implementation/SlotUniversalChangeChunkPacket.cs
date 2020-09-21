using DisplayButtons.Backend.Networking.Attributes;
using DisplayButtons.Backend.Networking.IO;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Bibliotecas;
using DisplayButtons.Properties;
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



        IDictionary<int, IDeckItem> toSend = new Dictionary<int, IDeckItem>();
        public void AddToQueue(int slot, IDeckItem item)
        {

       
            toSend.Add(slot, item);
        }
        public void ClearPacket()
        {

           

        }
        public override void FromInputStream(DataInputStream reader)
        {

        }

        public override long GetPacketNumber() => 12;

        public override void ToOutputStream(DataOutputStream writer)
        {
      
            writer.WriteInt(toSend.Count);
         //   writer.WriteUTF(jsonString);

           foreach (var item in toSend)
           {
              SendDeckLabel(writer, item.Key, item.Value) ;

             }

        }

        private void SendDeckLabel(DataOutputStream writer, int slot, IDeckItem item)
        {

            //Write the slot
            if (item.GetDeckDefaultLayer.SetDefault != null)
            {
                Json headerContent = new Json();

     
                writer.WriteInt(slot);
                //Byte array lenght
                writer.WriteInt(item.GetDeckDefaultLayer.SetDefault.InternalBitmap.Length);
                writer.Write(item.GetDeckDefaultLayer.SetDefault.InternalBitmap);

                headerContent.Font = " ";
                headerContent.Size = item.GetDeckDefaultLayer.Decksize;
                headerContent.Position = item.GetDeckDefaultLayer.Deckposition;
                headerContent.Text = item.GetDeckDefaultLayer.Deckname;
                headerContent.Color = item.GetDeckDefaultLayer.Deckcolor;

                headerContent.Stroke_color = item.GetDeckDefaultLayer.Stroke_color;
                headerContent.Stroke_dx = item.GetDeckDefaultLayer.Stroke_dxtext;
                headerContent.Stroke_radius = item.GetDeckDefaultLayer.Stroke_radius;
                headerContent.Stroke_dy = item.GetDeckDefaultLayer.Stroke_Dy;
                headerContent.IsStroke = item.GetDeckDefaultLayer.IsStroke;
                headerContent.Isboldtext = item.GetDeckDefaultLayer.Isboldtext;
                headerContent.Isnormaltext = item.GetDeckDefaultLayer.Isnormaltext;
                headerContent.Isitalictext = item.GetDeckDefaultLayer.Isitalictext;
                headerContent.Ishinttext = item.GetDeckDefaultLayer.Ishinttext;
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
