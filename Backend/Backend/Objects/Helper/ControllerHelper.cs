using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DisplayButtons.Bibliotecas.Helpers
{
   public static class ControllerHelper
    {
        public class GlobalDeviceControl
        {
            public string Text { get; set; }
            public CoreAudioDevice Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public static void SelectItemByValue(this ComboBox cbo, Guid guid)
        {

            if (guid != null)
            {
                foreach (ControllerHelper.GlobalDeviceControl item in cbo.Items)
                {

                    if (item.Value.Id == guid)
                    {
                        cbo.SelectedItem = item;
                        break;
                    }


                }
            }
        }
        public static CoreAudioDevice getDeviceByGuid(Guid guid)
        {
            CoreAudioDevice result = null;
            var audioController = new CoreAudioController();
            
            var devices = Task.FromResult(audioController.GetDevices(DeviceType.All, DeviceState.Active));
            foreach (CoreAudioDevice device in devices.Result)
            {
              if(device.Id == guid)
                {
                    result =  device;

                }
                
            }
            return result;
        }
    }
}
