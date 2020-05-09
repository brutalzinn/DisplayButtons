using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ServerToClient)]
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
