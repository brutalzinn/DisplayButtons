using DisplayButtons.Backend.Networking.IO;
using DisplayButtons.Backend.Networking.Attributes;
using DisplayButtons.Backend.Networking.TcpLib;
using DisplayButtons.Backend.Utils;

namespace DisplayButtons.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class AlternativeHello : INetworkPacket
    {
        public override void Execute(ConnectionState state)
        {
            state.SendPacket(new AlternativeHello());
            //state.EndConnection();
        }
   
        public override void FromInputStream(DataInputStream reader)
        {
            //From client

        }

        public override long GetPacketNumber() => 6;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //To client
            writer.WriteUTF(ApplicationSettingsManager.Settings.DeviceName);
        }

        public override object Clone()
        {
            return new AlternativeHello();
        }
    }
}
