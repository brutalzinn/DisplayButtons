using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Objects;
using ButtonDeck.Bibliotecas;
using Newtonsoft.Json;

namespace ButtonDeck.Backend.Networking.Implementation
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
        public string Color { get; set; }

        public string Text
        { get; set; }
        public int Id
        { get; set; }

        public string Font
        { get; set; }

        public int Size
        { get; set; }
        public int Position
        { get; set; }
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


                headerContent.Font = Font;
                headerContent.Size = Size;
                headerContent.Position = Position;
                headerContent.Text = Text;
                headerContent.Color = Color;
                string jsonString = JsonConvert.SerializeObject(headerContent, Formatting.None);

                writer.WriteUTF(jsonString);
            }
        }
    }
}
