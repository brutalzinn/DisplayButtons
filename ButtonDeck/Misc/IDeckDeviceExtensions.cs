using ButtonDeck.Backend.Networking;
using ButtonDeck.Backend.Networking.TcpLib;
using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Misc
{
    public static class IDeckDeviceExtensions
    {
        public static Guid GetConnectionGuidFromDeckDevice(DeckDevice device)
        {
            if(Program.mode == 0)
            {
 var connections = Program.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
            return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;


            }
            else
            {
                var connections = Program.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;



            }
        }
        public static ConnectionState GetConnection(this DeckDevice device)
        {
            if(Program.mode == 0)
            {

    var connections = Program.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
            var stateID = GetConnectionGuidFromDeckDevice(device);
            return connections.FirstOrDefault(m => m.ConnectionGuid == stateID);

            }
            else
            {

                var connections = Program.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                var stateID = GetConnectionGuidFromDeckDevice(device);
                return connections.FirstOrDefault(m => m.ConnectionGuid == stateID);


            }
        }

    }

}
