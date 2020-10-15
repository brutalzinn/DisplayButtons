using BackendAPI.Networking.Attributes;
using BackendAPI.Networking.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class SlotLabelButtonClearChunkPacket : INetworkPacket
    {
        readonly List<int> slotsToSend = new List<int>();

        public void AddToQueue(int slot)
        {
            slotsToSend.Add(slot);
        }

        public override void FromInputStream(DataInputStream reader)
        {
            
        }

        public override long GetPacketNumber() => 13;

        public override void ToOutputStream(DataOutputStream writer)
        {
            writer.WriteInt(slotsToSend.Count);
            foreach (var slot in slotsToSend) {
                writer.WriteInt(slot);
            }
        }
    }
}
