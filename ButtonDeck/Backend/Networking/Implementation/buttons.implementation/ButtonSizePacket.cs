using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Networking.Attributes;
using ButtonDeck.Backend.Networking.IO;
using ButtonDeck.Backend.Networking.TcpLib;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ButtonDeck.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class ButtonSizePacket : INetworkPacket
    {
        
        public override void Execute(ConnectionState state)
        {
         //   state.SendPacket(new AlternativeHello());
            //state.EndConnection();
        }

        public override void FromInputStream(DataInputStream reader)
        {
            //From client

        }

        public override long GetPacketNumber() => 11;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //To client
          writer.WriteInt(ApplicationSettingsManager.Settings.linha);
            writer.WriteInt(ApplicationSettingsManager.Settings.coluna);
        }

        public override object Clone()
        {
            return new MatrizPacket();
        }
    }
}