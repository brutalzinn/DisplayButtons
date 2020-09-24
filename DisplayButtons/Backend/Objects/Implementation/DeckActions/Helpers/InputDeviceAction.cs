﻿using DisplayButtons.Properties;
using DisplayButtons.Backend.Utils.Native;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.Helpers;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Diagnostics;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.General
{
    public class InputDeviceAction : AbstractDeckAction , IDeckHelper
    {
        public enum MediaInputDevice
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
        int CurrentItem = 1;
        IDeckItem atual_item;
        [ActionPropertyInclude]
        [ActionPropertyDescription("Device Control")]
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
            return DeckActionCategory.Helpers;
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
            return Texts.rm.GetString("HELPERSDECKINPUTDEVICE", Texts.cultereinfo);
        }
        public override bool IsLayered(int _current,IDeckItem item)
        {
            switch (Key)
            {
                case MediaInputDevice.Mute:
                    if (_current != -1)
                    {
                        CurrentItem = _current;
                        atual_item = item;
                        Debug.WriteLine("CURRENT ITEM ID IS " + CurrentItem);
                    }

                    return true;
                   
                default:
                    return false;

            }
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
          //  var device = ControllerHelper.getDeviceByGuid(DeviceId);
            switch (Key)
            {
                case MediaInputDevice.Mute:
                    if(device != null)
                    {  
                       
                   var result =  Task.FromResult( device.ToggleMuteAsync().Result);
                       IDeckHelper.setVariable(result.Result , atual_item, deckDevice);
                    }
                    break;
                case MediaInputDevice.Default:
                    if (device != null)
                    {
                        Task.FromResult(device.SetAsDefaultAsync());
                    }
                    break;
                case MediaInputDevice.VolumeUp:
                    if (device != null)
                    {
                        
                         volume = Math.Round(VolumeStepper + device.Volume);
                        Task.FromResult(device.SetVolumeAsync(volume));
                    }
                    break;
                case MediaInputDevice.VolumeDown:
                    if (device != null)
                    {

                        volume = Math.Round(device.Volume - VolumeStepper);
                        Task.FromResult(device.SetVolumeAsync(volume));
                    }
                    break;
         

            }
        }

     
    
    }
}
