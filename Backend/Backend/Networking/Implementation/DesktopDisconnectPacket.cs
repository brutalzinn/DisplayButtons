using Backend.Networking.IO;
using Backend.Networking.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class DesktopDisconnectPacket : INetworkPacket
    {
        public override object Clone()
        {
            return new DesktopDisconnectPacket();
        }
        public override void FromInputStream(DataInputStream reader)
        {
        }

        public override long GetPacketNumber() => 4;

        public override void ToOutputStream(DataOutputStream writer)
        {
        }
    }
}
