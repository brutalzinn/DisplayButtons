using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendAPI.Networking.IO;
using BackendAPI.Networking.Attributes;
using BackendAPI.Networking.TcpLib;
using BackendAPI.Utils;

namespace BackendAPI.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer)]
    public class ButtonInteractPacket : INetworkPacket
    {
        public ButtonAction PerformedAction { get; set; }
        public int SlotID { get; set; }
        public enum ButtonAction
        {
            None = -1,
            ButtonClick = 0,
            ButtonDown = 1,
            ButtonUp = 2
        }

        public override void FromInputStream(DataInputStream reader)
        {
            int actionID = reader.ReadInt();
            try {
                PerformedAction = (ButtonAction)actionID;
                SlotID = reader.ReadInt();
            } catch (Exception) {
                //Ignore malformed packet
            }

        }

        public override void Execute(ConnectionState state)
        {
            var device = DevicePersistManager.GetDeckDeviceFromConnectionGuid(state.ConnectionGuid);
            if (PerformedAction != ButtonAction.ButtonClick)
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
