

using BackendAPI.Networking.Implementation;
using BackendAPI.Sdk;
using BackendAPI.Utils;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackendAPI.Objects
{
    [Serializable]
    public class DeckDevice
    {

        public class ButtonInteractionEventArgs : EventArgs
        {
            public ButtonInteractionEventArgs(int slotID, ButtonShapes.ButtonAction performedAction)
            {
                SlotID = slotID;
                PerformedAction = performedAction;
            }

            public int SlotID { get; set; }
            public ButtonShapes.ButtonAction PerformedAction { get; set; }
        }


        /// <summary>
        /// Called to signal to subscribers that a button was interacted with
        /// </summary>
        public event EventHandler<ButtonInteractionEventArgs> ButtonInteraction;
        public virtual void OnButtonInteraction(ButtonShapes.ButtonAction performedAction, int slotID)
        {
            var eh = ButtonInteraction;

            eh?.Invoke(this, new ButtonInteractionEventArgs(slotID,
                performedAction));

        }

        public DeckDevice()
        { }

        public DeckDevice(Guid deviceGuid, string deviceName)
        {
            DeviceGuid = deviceGuid;
            DeviceName = deviceName;
    
           
       
            //if(CurrentProfile == null)
            //{
            //    return;
            //}
        }
        [XmlIgnore]
        public DeviceData DeviceUsb { get; set; }

        public Guid DeviceGuid { get; set; }
        public String DeviceName { get; set; }

        // public IDeckFolder MainFolder { get; set; }

        public List<Profile> profiles = new List<Profile>();
  
        [XmlIgnore]
        private  Profile _currentprofile;
         

        [XmlIgnore]
    public Profile CurrentProfile { get => _currentprofile; set => _currentprofile = value; }

        public virtual void CheckCurrentFolder()
        {
            if (CurrentProfile != null)
            {
                if (CurrentProfile.Mainfolder != null && CurrentProfile.Currentfolder == null)
                    CurrentProfile.Currentfolder = CurrentProfile.Mainfolder;
            }
        }
    }
}
