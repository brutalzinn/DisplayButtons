using DisplayButtons.Backend.Objects;
using DisplayButtons.Forms.EventSystem.Controls.actions;
using DisplayButtons.Forms.EventSystem.Controls.triggers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DisplayButtons.Forms;
using System.Linq;
using EventHook;

namespace DisplayButtons.Bibliotecas.DeckEvents.Actions
{
   
    public class TimerEvent : AbstractTrigger
    {
        public string AppName;
        public override string GetActionName()
        {

           
            return "Timer Event";
        }

        public override Type GetActionType()
        {
            throw new NotImplementedException();
        }

        public override void OnExecute()
        {
            throw new NotImplementedException();
        }
        public override AbstractTrigger CloneAction()
        {
            return new TimerEvent();
        }
        public void ToExecuteHelper()
        {
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;

        
            // form.comboBox.SelectedItem = GetActionName();
          form.Controls.Remove(form.comboBox);
            var instance = new TimerTrigger(this);
            form.UpdateForm(instance, 0);
   
            if (form.ShowDialog() == DialogResult.OK)
            {

                AppName = instance.textBox1.Text;
           
            }
            else
            {
                form.Close();
            }


        }
        public override PanelControl OnSelect()
        {

            return new TimerTrigger(this);
        }

        public override void OnInit()
        {
           
        }
    }
}
