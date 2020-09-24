using DisplayButtons.Properties;
using DisplayButtons.Backend.Utils.Native;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.Helpers;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System.ComponentModel.DataAnnotations;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.General
{
    public class OutputDeviceAction : AbstractDeckAction, IDeckHelper
    {
        
        public enum MediaOutputDevice
        {
            [Description("MISCMEDIAKEYSMUTE")]
            Mute,
            [Description("MISCMEDIAKEYSVOLUMEUP")]
            VolumeUp,
            [Description("MEDIASETDEFAULT")]
            Default,
            [Description("MISCMEDIAKEYSVOLUMEDOWN")]
            VolumeDown

        }

        [ActionPropertyInclude]
        [ActionPropertyDescription("Volume Stepper")]
        [ActionPropertyUpdateImageOnChanged]
        public int VolumeStepper { get; set; } = 10;
        [ActionPropertyInclude]
        [ActionPropertyDescription("Device Id")]
        [ActionPropertyUpdateImageOnChanged]
        public Guid DeviceId { get; set; }
        int CurrentItem = 1;
        IDeckItem atual_item;
        public double volume { get; set; }
        [ActionPropertyInclude]
        [ActionPropertyDescription("Media Key")]
        [ActionPropertyUpdateImageOnChanged]
        public MediaOutputDevice Key { get; set; } = MediaOutputDevice.Mute;

        public override AbstractDeckAction CloneAction()
        {
            
            return new OutputDeviceAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.Helpers;
        }
        public void DeviceIdHelper()
        {
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.AudioController.RecordDevices")) as Form;
            form.FillComboBox(DeviceType.Playback);
            form.selectDeviceById(DeviceId);
            if (form.ShowDialog() == DialogResult.OK)
            {


                DeviceId = ((ControllerHelper.GlobalDeviceControl)form.comboBox1.SelectedItem).Value.Id;
            }
            else
            {
                form.Close();
            }

        }
        public void VolumeStepperHelper()
        {
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.AudioController.VolumeStepper")) as Form;
            form.Volume_textbox.Text = VolumeStepper.ToString();
            if (form.ShowDialog() == DialogResult.OK)
            {


                VolumeStepper = Convert.ToInt32(form.Volume_textbox.Text);
            }
            else
            {
                form.Close();
            }

        }
        public override bool IsLayered(int _current, IDeckItem item)
        {
            switch (Key)
            {
                case MediaOutputDevice.Mute:
                    if (_current != -1)
                    {
                        CurrentItem = _current;
                        atual_item = item;
                      
                    }

                    return true;

                default:
                    return false;

            }
        }
        public override string GetActionName()
        {
            return Texts.rm.GetString("HELPERSDECKOUTPUTDEVICE", Texts.cultereinfo);
        }

        [Obsolete]
        public override bool OnButtonClick(DeckDevice deckDevice)
        {
            return false;
        }

       

        public override void OnButtonDown(DeckDevice deckDevice)
        {
           
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {

            var audioController = new CoreAudioController();
            var device = Task.FromResult(audioController.GetDevice(DeviceId, DeviceState.All)).Result;

            switch (Key)
            {
                case MediaOutputDevice.Mute:
                    if (device != null)
                    {
                        var result = Task.FromResult(device.ToggleMuteAsync().Result);
                        IDeckHelper.setVariable(result.Result, atual_item, deckDevice);
                    }
                    break;
                case MediaOutputDevice.Default:
                    if (device != null)
                    {
                        Task.FromResult(device.SetAsDefaultAsync());
                    }
                    break;
                case MediaOutputDevice.VolumeUp:
                    if (device != null)
                    {

                        volume = Math.Round(VolumeStepper + device.Volume);
                        Task.FromResult(device.SetVolumeAsync(volume));
                    }
                    break;
                case MediaOutputDevice.VolumeDown:
                    if (device != null)
                    {

                        volume = Math.Round(device.Volume - VolumeStepper);
                        Task.FromResult(device.SetVolumeAsync(volume));
                    }
                    break;


            
        }
        }

        public override DeckImage GetDefaultItemImage()
        {
            var img = GetKey(Key);
            if (img != null)
                return new DeckImage(img);
            return base.GetDefaultItemImage();
        }

        private Bitmap GetKey(MediaOutputDevice key)
        {
            switch (key) {
                case MediaOutputDevice.Mute:
                    return Resources.img_item_default;
               
                default:
                    return null;
            }
        }
    }
}
