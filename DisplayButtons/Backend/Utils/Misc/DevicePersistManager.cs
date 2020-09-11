using DisplayButtons.Backend.Networking.TcpLib;
using DisplayButtons.Backend.Objects;

using NHotkey.WindowsForms;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayButtons.Backend.Utils
{
    public static class DevicePersistManager
    {
        private const string DEVICES_FILENAME = "devices.xml";
        public static bool IsVirtualDeviceConnected { get; set; }
        public static DeviceData DeviceUsb { get; set; }

        private static IDictionary<Guid, DeckDevice> deckDevicesFromConnection = new Dictionary<Guid, DeckDevice>();

        public static IDictionary<Guid, DeckDevice> DeckDevicesFromConnection {
            get {
                return deckDevicesFromConnection;
            }
        }

        public class DeviceEventArgs : EventArgs
        {
            public DeviceEventArgs(DeckDevice device)
            {
                Device = device;
            }

            public DeckDevice Device { get; set; }
        }

        /// <summary>
        /// Called to signal to subscribers that a device was connected
        /// </summary>
        public static event EventHandler<DeviceEventArgs> DeviceConnected;
        public static void OnDeviceConnected(object sender, DeckDevice e)
        {
            var eh = DeviceConnected;

            eh?.Invoke(sender, new DeviceEventArgs(e));
        }

        /// <summary>
        /// Called to signal to subscribers that a device was disconnected
        /// </summary>
        public static event EventHandler<DeviceEventArgs> DeviceDisconnected;
        public static void OnDeviceDisconnected(object sender, DeckDevice e)
        {
            var eh = DeviceDisconnected;

            eh?.Invoke(sender, new DeviceEventArgs(e));
        }


        public static ICollection<Guid> GuidsFromConnections {
            get {
                return deckDevicesFromConnection.Keys;
            }
        }

        private static List<DeckDevice> persistedDevices;

        public static List<DeckDevice> PersistedDevices {
            get {
                return persistedDevices;
            }
        }
        
        public static Guid GetConnectionGuidFromDeckDevice(DeckDevice device)
        {
            return deckDevicesFromConnection.FirstOrDefault(m => m.Value.DeviceGuid == device.DeviceGuid).Key;
        }

        public static DeckDevice GetDeckDeviceFromConnectionGuid(Guid connection)
        {
            return deckDevicesFromConnection.FirstOrDefault(m => m.Key == connection).Value;
        }


        public static bool IsDeviceOnline(DeckDevice device)
        {
            
            return deckDevicesFromConnection.Values.Any(m => m.DeviceGuid == device.DeviceGuid);
        }


        [Obsolete]
        public static bool IsDeviceOnline(Guid device)
        {
            return deckDevicesFromConnection.Values.Any(m => m.DeviceGuid == device);
        }

        public static bool IsDeviceConnected(Guid device)
        {
            return deckDevicesFromConnection.Keys.Contains(device);
        }

        public static bool IsDevicePersisted(DeckDevice device)
        {
            return IsDevicePersisted(device.DeviceGuid);
        }

        private static bool IsDevicePersisted(Guid deviceGuid)
        {
            return PersistedDevices.Any(w => w.DeviceGuid == deviceGuid);
        }

        public static bool HasPerfilCreated(DeckDevice device)
        {

           if(device.CurrentProfile != null)
            {

                return true;
            }
            else
            {
                return false;
            }

        }
            public static void PersistDevice(DeckDevice device)
        {

            if (IsDevicePersisted(device))
            {

                device.DeviceName = PersistedDevices.First(m => m.DeviceGuid == device.DeviceGuid).DeviceName;
                if (device.CurrentProfile != null)
                {
                    device.CurrentProfile.Mainfolder = PersistedDevices.First(m => m.DeviceGuid == device.DeviceGuid).CurrentProfile.Mainfolder;
                }
                else
                {
                    return;
                }
                PersistedDevices.RemoveAll(m => m.DeviceGuid == device.DeviceGuid);

            }
 PersistedDevices.Add(device);

            
           
            
        }
        public static void PersistUsbGuid(DeviceData device_usb, Guid serial)
        {
            device_usb.Serial = serial.ToString();
        }
        public static void PersistUsbMode(DeviceData device_usb)
        {
            DeviceUsb = device_usb;
        }
        public static void RemoveConnectionState(ConnectionState state)
        {
            if (deckDevicesFromConnection.Keys.Contains(state.ConnectionGuid)) {
                var device = GetDeckDeviceFromConnectionGuid(state.ConnectionGuid);
                OnDeviceDisconnected(new object(), device);
            }
            deckDevicesFromConnection.Remove(state.ConnectionGuid);
        }

        public static void RemoveConnectionState(Guid state)
        {
            deckDevicesFromConnection.Remove(state);
        }

        public static void ChangeConnectedState(ConnectionState state, DeckDevice device)
        {
            if (device == null || state == null) return;
            if (!deckDevicesFromConnection.ContainsKey(state.ConnectionGuid)) {
                deckDevicesFromConnection.Add(state.ConnectionGuid, device);
            }
        }

        public static void LoadDevices()
        {
            if (File.Exists(DEVICES_FILENAME)) {
                var newPersistedDevices = XMLUtils.FromXML<List<DeckDevice>>(File.ReadAllText(DEVICES_FILENAME));
                if (persistedDevices == null) persistedDevices = new List<DeckDevice>();
                persistedDevices.AddRange(newPersistedDevices.Where(m => !IsDevicePersisted(m.DeviceGuid)));
            } else {
                persistedDevices = new List<DeckDevice>();
            }
        }

        private static void CompressFolders(IDeckFolder folder)
        {
            folder.GetSubFolders().All(c => {
                CompressFolders(c);
                c.SetParent(folder);
                if (c.GetParent() != null) {
                
                    c.Remove(1);
                }

                return true;
            
            });
        }

        //public static void SaveProfileItems()
        //{
        //    foreach (var device in persistedDevices)
        //    {
        //        if (device.CurrentProfile != null)
        //        {
        //            device.CurrentProfile.Mainfolder = device.CurrentProfile.Currentfolder;
        //        }
        //    }
        //}
        public static void SaveDevices()
        {
            //  SaveProfileItems();
           
                foreach (var device in persistedDevices)
                {

                if (device.CurrentProfile != null)
                {
                    CompressFolders(device.CurrentProfile.Mainfolder);
                }
             
                }

                if (persistedDevices != null)
                {
                    File.WriteAllText(DEVICES_FILENAME, XMLUtils.ToXML(persistedDevices));
                }
                else
                {
                    Debug.WriteLine("DELETE " + DEVICES_FILENAME);
                    //    File.Delete(DEVICES_FILENAME);
                }
            
        }
           
        }
    }

