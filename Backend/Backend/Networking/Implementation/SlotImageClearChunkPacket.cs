using Backend.Networking.Attributes;
using Backend.Networking.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SlotImageClearChunkPacket : INetworkPacket
    {
        readonly List<int> slotsToSend = new List<int>();

        public void AddToQueue(int slot)
        {
            slotsToSend.Add(slot);
        }

        public override void FromInputStream(DataInputStream reader)
        {
            
        }

        public override long GetPacketNumber() => 10;

        public override void ToOutputStream(DataOutputStream writer)
        {
            writer.WriteInt(slotsToSend.Count);
            foreach (var slot in slotsToSend) {
                writer.WriteInt(slot);
            }
        }
    }
}
