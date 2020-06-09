using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SlotLabelChangeChunk : INetworkPacket
    {
        IDictionary<int, string> toSend = new Dictionary<int, string>();

        public void AddToQueue(int slot, string text)
        {
            toSend.Add(slot, text);
        }

        public override void FromInputStream(DataInputStream reader)
        {
        }

        public override long GetPacketNumber() => 7;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //The number of images to send
            writer.WriteInt(toSend.Count);
            foreach (var item in toSend)
            {
                SendDeckImage(writer, item.Key, item.Value);
            }

        }

        private void SendDeckImage(DataOutputStream writer, int slot, string text)
        {
            if (text != null)
            {
                //Write the slot
                writer.WriteInt(slot);
                //Write the byte array lenght
                writer.WriteInt(text.Length);
                //Write the byte array
                writer.WriteUTF(text);
            }
        }

        public override object Clone()
        {
            return new SlotLabelChangeChunk();
        }
    }
}
