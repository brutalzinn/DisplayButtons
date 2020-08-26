using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayButtons.Backend.Networking.IO;
using DisplayButtons.Backend.Networking.Attributes;
using DisplayButtons.Backend.Networking.IO;
using DisplayButtons.Backend.Networking.TcpLib;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Utils;
using System.Diagnostics;
using DisplayButtons.Forms;
using DisplayButtons.Backend.Objects.Implementation;

namespace DisplayButtons.Backend.Networking.Implementation
{
    [Architecture(PacketArchitecture.ClientToServer | PacketArchitecture.ServerToClient)]
    public class DeviceIdentityPacket : INetworkPacket
    {
        public DeviceIdentityPacket()
        {

        }
        private readonly bool hasDeviceGuid;

        public DeviceIdentityPacket(bool hasDeviceGuid)
        {
            this.hasDeviceGuid = hasDeviceGuid;
        }

       [ServerOnly]
        public Guid DeviceGuid { get; set; }

      [ClientOnly]
        public string DeviceName { get; set; }

        public override long GetPacketNumber() => 2;

        public override void FromInputStream(DataInputStream reader)
        {
            string receivedGuid = reader.ReadUTF().ToUpperInvariant();
 
           DeviceGuid = new Guid(receivedGuid);
              DeviceName = reader.ReadUTF();
        }

        public override void Execute(ConnectionState state)
        {

                

            DeckDevice deckDevice = new DeckDevice(DeviceGuid, DeviceName);

            DevicePersistManager.PersistDevice(deckDevice);
            DevicePersistManager.ChangeConnectedState(state, deckDevice);
            /*
            var deckImage = new DeckImage(new System.Drawing.Bitmap("streamdeck_key.png"));
            var packet = new SlotImageChangeChunkPacket();
            packet.AddToQueue(1, deckImage);
            packet.AddToQueue(3, deckImage);
            packet.AddToQueue(5, deckImage);
            packet.AddToQueue(15, deckImage);
            state.SendPacket(packet);*/
             
            if (Program.mode == 1)
            {
//                UsbMode teste = new UsbMode();
              
          MainForm.Instance.StartUsbMode();  
//MainForm.Instance.CurrentDevice = deckDevice;
//                //   teste.MountUsbDevices();




//                teste.MountUsbDevices();
            }
            DevicePersistManager.OnDeviceConnected(this, deckDevice);
            Debug.WriteLine("MOSTRANDO GUID PARA: " + DeviceName);
            Debug.WriteLine("MOSTRANDO GUID PARA: " + DeviceGuid);


        }


        public override void ToOutputStream(DataOutputStream writer)
        {
            //From server to client
            //Tell the client if they are going to receive a device Guid
            writer.WriteBoolean(!hasDeviceGuid);
            if (!hasDeviceGuid) {
                DeviceGuid = Guid.NewGuid();
                writer.WriteUTF(DeviceGuid.ToString());
            }
        }

        public override object Clone()
        {
            return new DeviceIdentityPacket();
        }
    }
}
