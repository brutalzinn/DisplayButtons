using BackendAPI;
using BackendAPI.Networking;
using BackendAPI.Networking.TcpLib;
using BackendAPI.Objects;
using BackendAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc
{
    public static class IDeckDeviceExtensions
    {
        public static Guid GetConnectionGuidFromDeckDevice(DeckDevice device)
        {
            if (Initilizator.mode== 0)
            {
 var connections = Initilizator.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
            return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;


            }
            else
            {
                var connections = Initilizator.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                return DevicePersistManager.DeckDevicesFromConnection.Where(m => connections.Select(c => c.ConnectionGuid).Contains(m.Key)).FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;



            }
        }
        public static ConnectionState GetConnection(this DeckDevice device)
        {
            if(Initilizator.mode == 0)
            {

    var connections = Initilizator.ServerThread.TcpServer?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
            var stateID = GetConnectionGuidFromDeckDevice(device);
            return connections.FirstOrDefault(m => m.ConnectionGuid == stateID);

            }
            else
            {

                var connections = Initilizator.ClientThread.TcpClient?.Connections.OfType<ConnectionState>().Where(c => c.IsStillFunctioning());
                var stateID = GetConnectionGuidFromDeckDevice(device);
                return connections.FirstOrDefault(m => m.ConnectionGuid == stateID);


            }
        }

    }

}
