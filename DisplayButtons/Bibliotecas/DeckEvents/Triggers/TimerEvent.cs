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
using System.Diagnostics;
using System.Reflection;

namespace DisplayButtons.Bibliotecas.DeckEvents.Actions
{
   
    public class TimerEvent : AbstractTrigger
    {
        public string AppName;
        public bool recurring;

        public DateTime Start = DateTime.Now;
        public TimeSpan Interval;
        public override string GetActionName()
        {

           
            return "Timer Event";
        }

        public override Type GetActionType()
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
            
                if (instance.recurring_timer_radio.Checked)
                {
                    Interval = TimeSpan.Parse(instance.textBox1.Text);
                    recurring = true;
                }
                if (instance.Datetime_radio.Checked)
                {

    Start = instance.dateTimePicker1.Value;
                    recurring = false;
                }
               
              //  AppName = instance.textBox1.Text;

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


        public override void OnInit(Event value)
        {
            SharpShedule.Sheduler teste = new SharpShedule.Sheduler();

            if (recurring)
            {
                teste.RunIn(OnExecute, Interval, value);
            }
            else
            {
                teste.Shedule(OnExecute, Start, value, Interval);
                
            } 
          teste.Start();
        }
     
        private void TimerEvent_OnError(object sender, Exception e)
        {
            Debug.WriteLine(e);
        }
    }

}
