using Backend;
using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.TcpLib;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayButtons.Misc
{
    public static class IDeckDeviceExtensions
    {
        public static Guid GetConnectionGuidFromDeckDevice(DeckDevice device)
        {
            if(Config.mode == 0)
            {
 var connections = Config.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
            return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;


            }
            else
            {
                var connections = Config.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;



            }
        }
        public static ConnectionState GetConnection(this DeckDevice device)
        {
            if(Config.mode == 0)
            {

    var connections = Config.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
            var stateID = GetConnectionGuidFromDeckDevice(device);
            return connections.FirstOrDefault(m => m.ConnectionGuid == stateID);

            }
            else
            {

                var connections = Config.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                var stateID = GetConnectionGuidFromDeckDevice(device);
                return connections.FirstOrDefault(m => m.ConnectionGuid == stateID);


            }
        }

    }

}
