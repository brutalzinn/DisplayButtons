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
                }
                if (instance.Datetime_radio.Checked)
                {

    Start = instance.dateTimePicker1.Value;
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
   
        void Run()
        {

            Console.WriteLine("TESTEEE");
            MessageBox.Show("DEU RUIK");
        }


        public void eventHelper(Event events)
        {
            Action dividir = OnExecute(events);
            if (recurring)
            {
               new SharpShedule.SheduleBase().RunIn(dividir, Interval);
            }
            else
            {
                new SharpShedule.SheduleBase().Shedule(dividir, Start, TimeSpan.FromMilliseconds(1000));

            }
                 
        }
        public override void OnInit()
        {

           
           

        }
       
    }
}
