using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Networking.Attributes;
using Backend.Networking.IO;
using Backend.Objects;

namespace Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ServerToClient)]
    public class SingleSlotImageChangePacket : INetworkPacket
    {
        public SingleSlotImageChangePacket()
        { }
        public SingleSlotImageChangePacket(DeckImage deckImage)
        {
            DeckImage = deckImage;
        }

        public DeckImage DeckImage { get; set; }
        public int ImageSlot { get; set; }

        public override object Clone()
        {
            return new SingleSlotImageChangePacket();
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
            }
        }
    }
}
