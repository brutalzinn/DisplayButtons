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
 

       public DeckItemMisc CurrentItem { get; set; }
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
                Json headerContent = new Json
                {
                    Font = " ",
                    Size = CurrentItem.Decksize,
                    Position = CurrentItem.Deckposition,
                    Text = CurrentItem.Deckname,
                    Color = CurrentItem.Deckcolor,
                    Stroke_color = CurrentItem.Stroke_color,
                    Stroke_dx = CurrentItem.Stroke_dxtext,
                    Stroke_radius = CurrentItem.Stroke_radius,
                    Stroke_dy = CurrentItem.Stroke_Dy,
                    IsStroke = CurrentItem.IsStroke,
                    Isboldtext = CurrentItem.Isboldtext,
                    Isnormaltext = CurrentItem.Isnormaltext,
                    Isitalictext = CurrentItem.Isitalictext,
                    Ishinttext = CurrentItem.Ishinttext
                };
                string jsonString = JsonConvert.SerializeObject(headerContent, Formatting.None);

                writer.WriteUTF(jsonString);
            }
        }
    }
}
