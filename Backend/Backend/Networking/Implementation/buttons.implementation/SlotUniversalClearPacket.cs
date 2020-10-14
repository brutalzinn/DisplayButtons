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
    public class SlotUniversalClearPacket : INetworkPacket
    {
        public SlotUniversalClearPacket(int slotID)
        {
            SlotID = slotID;
        }

        public int SlotID { get; set; }
        public override void FromInputStream(DataInputStream reader)
        {
            
        }

        public override long GetPacketNumber() => 9;

        public override void ToOutputStream(DataOutputStream writer)
        {
            writer.WriteInt(SlotID);   
        }
    }
}
