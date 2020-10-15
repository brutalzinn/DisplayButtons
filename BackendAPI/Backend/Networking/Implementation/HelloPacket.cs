using BackendAPI.Networking.Attributes;
using BackendAPI.Networking.IO;
using BackendAPI.Networking.TcpLib;
using BackendAPI.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI.Networking.Implementation
{
   // [Architecture(PacketArchitecture.ClientToServer)]
    public class HelloPacket : INetworkPacket
    {
        public int ProtocolVersion { get; set; }

        private bool HasDeviceGuid { get; set; }
        public Guid DeviceGuid { get; set; }

        public override long GetPacketNumber() => 1;


        public override void Execute(ConnectionState state)
        {
            Debug.WriteLine("EXECUTE  ");
            if (ProtocolVersion != Constants.PROTOCOL_VERSION) {
        //  state.EndConnection();
            } else {
                state.SendPacket(new DeviceIdentityPacket(HasDeviceGuid));
            // state.SendPacket(new AlternativeHello());

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
