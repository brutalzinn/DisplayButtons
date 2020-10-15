
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using Backend;
using DisplayButtons.Bibliotecas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DisplayButtons.Bibliotecas.Helpers.ControllerHelper;

namespace DisplayButtons.Forms.AudioController
{
    public partial class RecordDevices : TemplateForm
    {
        public RecordDevices()
        {
            InitializeComponent();
            save_button.Text = Texts.rm.GetString("EVENTSYSTEMSAVEBUTTON", Texts.cultereinfo);
            cancel_button.Text = Texts.rm.GetString("EVENTSYSTEMCANCELBUTTON", Texts.cultereinfo);

        }
        public void FillComboBox(DeviceType item)
        {
  var audioController = new CoreAudioController();
            var devices = Task.FromResult(audioController.GetDevices(item, DeviceState.Active)) ;
            foreach (var device in devices.Result)
            {
                GlobalDeviceControl dd = new GlobalDeviceControl();
                dd.Text = device.FullName;
                dd.Value = device;
           
                comboBox1.Items.Add(dd);
              
            }

        }
        public void selectDeviceById(Guid id)
        {
            ControllerHelper.SelectItemByValue(comboBox1,id);
            
        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }
        private void RecordDevices_Load(object sender, EventArgs e)
        {

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }
    }
}
