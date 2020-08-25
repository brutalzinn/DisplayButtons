using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Utils;

using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Objects
{
    [Serializable]
    public class DeckDevice
    {

        public class ButtonInteractionEventArgs : EventArgs
        {
            public ButtonInteractionEventArgs(int slotID, ButtonInteractPacket.ButtonAction performedAction)
            {
                SlotID = slotID;
                PerformedAction = performedAction;
            }

            public int SlotID { get; set; }
            public ButtonInteractPacket.ButtonAction PerformedAction { get; set; }
        }


        /// <summary>
        /// Called to signal to subscribers that a button was interacted with
        /// </summary>
        public event EventHandler<ButtonInteractionEventArgs> ButtonInteraction;
        public virtual void OnButtonInteraction(ButtonInteractPacket.ButtonAction performedAction, int slotID)
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
    
            // CurrentProfile.Mainfolder = new Implementation.DynamicDeckFolder();

            //Profile profile = new Profile("DEFAULT", matriz, new Implementation.DynamicDeckFolder());

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
        public Profile CurrentProfile { 
            
            
            get {
                if(_currentprofile == null)
                {
                    Profile teste = new Profile("DEFAULT", new MatrizObject(0, 0), new Implementation.DynamicDeckFolder());
                    _currentprofile = teste;
                }
                
                return _currentprofile;
            
            }



            set
            {
                _currentprofile = value;
            }
        
        }
        public virtual void CheckCurrentFolder()
        {
            if (CurrentProfile.Mainfolder != null && CurrentProfile.Currentfolder == null)
                CurrentProfile.Currentfolder = CurrentProfile.Mainfolder;
        }
    }
}
