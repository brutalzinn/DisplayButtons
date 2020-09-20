﻿using DisplayButtons.Properties;
using DisplayButtons.Backend.Utils.Native;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.Helpers;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.General
{
    public class InputDeviceAction : AbstractDeckAction
    {
        public enum MediaInputDevice
        {
            [Description("Mute")]
            Mute,
            [Description("Volume up")]
            VolumeUp,
            [Description("Set Default")]
            Default,
            [Description("Volume down")]
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
        public MediaInputDevice Key { get; set; } = MediaInputDevice.Mute;

        public override AbstractDeckAction CloneAction()
        {
            return new InputDeviceAction();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.General;
        }
        public void DeviceIdHelper()
        {
            dynamic form = Activator.CreateInstance(FindType("DisplayButtons.Forms.AudioController.RecordDevices")) as Form;
            form.FillComboBox(DeviceType.Capture);
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
            return "Input device control";
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

            var device = ControllerHelper.getDeviceByGuid(DeviceId);
            switch (Key)
            {
                case MediaInputDevice.Mute:
                    if(device != null)
                    {
                    Task.FromResult( device.SetMuteAsync(!device.IsMuted));
                    }
                    break;
                case MediaInputDevice.Default:
                    if (device != null)
                    {
                        Task.FromResult(device.SetAsDefault());
                    }
                    break;
                case MediaInputDevice.VolumeUp:
                    if (device != null)
                    {
                        
                         volume = Math.Round(sensitivevolume + device.Volume);
                        Task.FromResult(device.SetVolumeAsync(volume));
                    }
                    break;
                case MediaInputDevice.VolumeDown:
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

        private Bitmap GetKey(MediaInputDevice key)
        {
            switch (key) {
                case MediaInputDevice.Mute:
                    return Resources.img_item_default;
               
                default:
                    return null;
            }
        }
    }
}
