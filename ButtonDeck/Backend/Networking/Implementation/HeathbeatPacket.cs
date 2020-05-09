using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ServerToClient)]
    public class HeartbeatPacket : INetworkPacket
    {
        public override void FromInputStream(DataInputStream reader)
        {
        }

        public override long GetPacketNumber() => 3;

        public override object Clone()
        {
            return new HeartbeatPacket();
        }

        public override void ToOutputStream(DataOutputStream writer)
        {
        }
    }
}