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
    public class OutputDeviceAction : AbstractDeckAction
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

        int sensitivevolume = 10;
        [ActionPropertyInclude]
        [ActionPropertyDescription("Device Id")]
        [ActionPropertyUpdateImageOnChanged]
        public Guid DeviceId { get; set; }

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
        public override string GetActionName()
        {
            return "Output device control";
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
                        Task.FromResult(device.ToggleMuteAsync());
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

                        volume = Math.Round(sensitivevolume + device.Volume);
                        Task.FromResult(device.SetVolumeAsync(volume));
                    }
                    break;
                case MediaOutputDevice.VolumeDown:
                    if (device != null)
                    {

                        volume = Math.Round(device.Volume - sensitivevolume);
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
