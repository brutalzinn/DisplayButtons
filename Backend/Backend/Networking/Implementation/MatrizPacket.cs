using Backend.Networking.IO;
using Backend.Networking.Attributes;
using Backend.Networking.TcpLib;
using Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class MatrizPacket : INetworkPacket
    {
        
        public override void Execute(ConnectionState state)
        {
         //   state.SendPacket(new AlternativeHello());
            //state.EndConnection();
        }
        public MatrizPacket()
        {
          

        }
        public MatrizPacket(Profile _profile)
        {
            profile = _profile;

        }
        private Profile profile { get; set; }
        public override void FromInputStream(DataInputStream reader)
        {

            Globals.status = reader.ReadBoolean();
            Debug.WriteLine("RECEBIDO. " + Globals.status.ToString());
        }

        public override long GetPacketNumber() => 11;

        public override void ToOutputStream(DataOutputStream writer)
        {
            //To client
           
                writer.WriteInt(profile.Matriz.Lin);
                writer.WriteInt(profile.Matriz.Column);
           
        }

        public override object Clone()
        {
            return new MatrizPacket();
        }
    }
}