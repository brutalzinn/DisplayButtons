using ButtonDeck.Backend.Networking.Implementation;
using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ButtonDeck.Backend.Networking.TcpLib
{
    public class DeckServiceProvider : TcpServiceProvider
    {

        private static List<INetworkPacket> networkPackets = new List<INetworkPacket>();

        private static System.Timers.Timer aTimer = new System.Timers.Timer();
        static DeckServiceProvider()
        {
            RegisterNetworkPacket(new HelloPacket());
            RegisterNetworkPacket(new DeviceIdentityPacket());
            RegisterNetworkPacket(new HeartbeatPacket());
            RegisterNetworkPacket(new DesktopDisconnectPacket());
            RegisterNetworkPacket(new AlternativeHello());
            RegisterNetworkPacket(new ButtonInteractPacket());
            RegisterNetworkPacket(new MatrizPacket());
            //RegisterNetworkPacket(new SlotLabelButtonChangeChunkPacket());
            //RegisterNetworkPacket(new SlotLabelButtonClearChunkPacket());
            //    RegisterNetworkPacket(new UsbInteractPacket());
        }

        public static void RegisterNetworkPacket(INetworkPacket packet)
        {
            networkPackets.Add(packet);
        }

        public static INetworkPacket GetNewNetworkPacketById(long id)
        {
            try {
                return (INetworkPacket)networkPackets.FirstOrDefault(p => p.GetPacketNumber() == id).Clone();
            } catch (Exception) {
                throw new Exception($"NetworkPacket[ID: {id}] wasn't registered to the packet storage.");
            }
        }

        public override void OnAcceptConnection(ConnectionState state)
        {
         aTimer.Enabled = false;
        }
        public override void OnRetryConnect(ConnectionState state)
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 3000;
            aTimer.Enabled = true;
            UsbMode devices_refresh = new UsbMode();
            devices_refresh.MountUsbDevices();
            Debug.WriteLine("Tentando reconexão.." + aTimer.Interval);



        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            UsbMode devices_refresh = new UsbMode();
            devices_refresh.RefreshCurrentUsb();
            
        }
        public override void OnDropConnection(ConnectionState state)
        {
           // state.SendPacket(new DesktopDisconnectPacket());
         DevicePersistManager.RemoveConnectionState(state);
        }

        public override void OnReceiveData(ConnectionState state)
        {
            List<byte> allData = new List<byte>();
            byte[] buffer;
            Debug.WriteLine("AvailiableData: " + state.AvailableData);
            
            while (state.AvailableData > 0) {
                buffer = new byte[1024];
                state.Read(buffer, 0, 1024);
                allData.AddRange(buffer);
            }
            var packet = state.ReadPacket(allData.ToArray());

        }
        public override object Clone()
        {
            return new DeckServiceProvider();
        }

    }
}