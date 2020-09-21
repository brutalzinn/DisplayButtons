using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayButtons.Backend.Networking.Attributes;
using DisplayButtons.Backend.Networking.IO;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Bibliotecas;
using Newtonsoft.Json;

namespace DisplayButtons.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SingleUniversalChangePacket : INetworkPacket
    {
        public SingleUniversalChangePacket()
        { }
        public SingleUniversalChangePacket(DeckImage deckImage)
        {
            DeckImage = deckImage;
        }

        public DeckImage DeckImage { get; set; }
        public int ImageSlot { get; set; }
 

       public IDeckItem CurrentItem { get; set; }
        public override object Clone()
        {
            return new SingleUniversalChangePacket();
        }

        public override void FromInputStream(DataInputStream reader)
        {
            //Client to Server 
        }

        public override long GetPacketNumber() => 5;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //Server to Client
            writer.WriteBoolean(DeckImage != null);
            if (DeckImage != null) {
                writer.WriteInt(ImageSlot);
                //Byte array lenght
                writer.WriteInt(DeckImage.InternalBitmap.Length);
                writer.Write(DeckImage.InternalBitmap);
                Json headerContent = new Json();

               
                headerContent.Font = " ";
                headerContent.Size = CurrentItem.GetDeckDefaultLayer.Decksize;
                headerContent.Position = CurrentItem.GetDeckDefaultLayer.Deckposition;
                headerContent.Text = CurrentItem.GetDeckDefaultLayer.Deckname;
                headerContent.Color = CurrentItem.GetDeckDefaultLayer.Deckcolor;
                headerContent.Stroke_color = CurrentItem.GetDeckDefaultLayer.Stroke_color;
                headerContent.Stroke_dx = CurrentItem.GetDeckDefaultLayer.Stroke_dxtext;
                headerContent.Stroke_radius = CurrentItem.GetDeckDefaultLayer.Stroke_radius;
                headerContent.Stroke_dy = CurrentItem.GetDeckDefaultLayer.Stroke_Dy;
                headerContent.IsStroke = CurrentItem.GetDeckDefaultLayer.IsStroke;
                headerContent.Isboldtext = CurrentItem.GetDeckDefaultLayer.Isboldtext;
                headerContent.Isnormaltext = CurrentItem.GetDeckDefaultLayer.Isnormaltext;
                headerContent.Isitalictext = CurrentItem.GetDeckDefaultLayer.Isitalictext;
                headerContent.Ishinttext = CurrentItem.GetDeckDefaultLayer.Ishinttext;
                string jsonString = JsonConvert.SerializeObject(headerContent, Formatting.None);

                writer.WriteUTF(jsonString);
            }
        }
    }
}
