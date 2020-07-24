using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Objects;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SingleSlotLabelChangePacket : INetworkPacket
    {
        public SingleSlotLabelChangePacket()
        { }
        public SingleSlotLabelChangePacket(int slot, string font, int size, int pos, string text, string color)
        {
            
        }
       
        public DeckImage DeckImage { get; set; }
        public int ImageSlot { get; set; }

        public override object Clone()
        {
            return new SingleSlotLabelChangePacket();
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
