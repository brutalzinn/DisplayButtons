using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Networking.TcpLib;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class HelloPacket : INetworkPacket
    {
        public int ProtocolVersion { get; set; }

        private bool HasDeviceGuid { get; set; }
        public Guid DeviceGuid { get; set; }

        public override long GetPacketNumber() => 1;


        public override void Execute(ConnectionState state)
        {
            if (ProtocolVersion != Constants.PROTOCOL_VERSION) {
                state.EndConnection();
            } else {
                state.SendPacket(new DeviceIdentityPacket(HasDeviceGuid));
            }
        }

        public override object Clone()
        {
            return new HelloPacket();
        }

        public override void FromInputStream(DataInputStream reader)
        {
            ProtocolVersion = reader.ReadInt();
            HasDeviceGuid = reader.ReadBoolean();
            if (HasDeviceGuid) {
                //We have a device GUID
                DeviceGuid = Guid.Parse(reader.ReadUTF().ToUpper());
            }
        }

        public override void ToOutputStream(DataOutputStream writer)
        {
        }
    }
}
