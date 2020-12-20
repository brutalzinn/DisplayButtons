using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendAPI.Networking.IO;
using BackendAPI.Networking.Attributes;
using BackendAPI.Networking.TcpLib;
using BackendAPI.Utils;
using BackendAPI.Sdk;

namespace BackendAPI.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class ButtonInteractPacket : INetworkPacket
    {
        public ButtonShapes.ButtonAction PerformedAction { get; set; }
        public int SlotID { get; set; }
    

        public override void FromInputStream(DataInputStream reader)
        {
            int actionID = reader.ReadInt();
            try {
                PerformedAction = (ButtonShapes.ButtonAction)actionID;
                SlotID = reader.ReadInt();
            } catch (Exception) {
                //Ignore malformed packet
            }

        }

        public override void Execute(ConnectionState state)
        {
            var device = DevicePersistManager.GetDeckDeviceFromConnectionGuid(state.ConnectionGuid);
            if (PerformedAction != ButtonShapes.ButtonAction.ButtonClick)
                device.OnButtonInteraction(PerformedAction, SlotID);
        }
  
        public override long GetPacketNumber() => 8;

        public override void ToOutputStream(DataOutputStream writer)
        {

        }

        public override object Clone()
        {
            return new ButtonInteractPacket();
        }
    }
}
