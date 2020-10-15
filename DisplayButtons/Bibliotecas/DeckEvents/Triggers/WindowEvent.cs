using DisplayButtons.Backend.Objects;
using DisplayButtons.Forms.EventSystem.Controls.actions;
using DisplayButtons.Forms.EventSystem.Controls.triggers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DisplayButtons.Forms;
using System.Linq;

using System.Diagnostics;
using CookieProjects.ProcessWatcher;
using Backend.Objects;

namespace DisplayButtons.Bibliotecas.DeckEvents.Actions
{
   
    public class WindowEvent : AbstractTrigger
    {
        public string AppName;
        public int windowEvent;
     
        public override string GetActionName()
        {

           
            return "Window Event";
        }

        public override Type GetActionType()
        {
            throw new NotImplementedException();
        }

       
        public override AbstractTrigger CloneAction()
        {
            return new WindowEvent();
        }

        public void ProcessHelper(Process process, int status, Event events)
        {

            Debug.WriteLine(String.Format("The process '{0}' is called, status: {1}", process.ProcessName, status));
       
        if(process.ProcessName == AppName && status == windowEvent)
            {

                OnExecute(events);
            }
        
        }
            public void ToExecuteHelper()
        {
            dynamic form = Activator.CreateInstance(AbstractDeckAction.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;

        
            // form.comboBox.SelectedItem = GetActionName();
          form.Controls.Remove(form.comboBox);
            var instance = new WindowTrigger(this);
            form.UpdateForm(instance, 0);
   
            if (form.ShowDialog() == DialogResult.OK)
            {
                int value = ((KeyValuePair<int, string>)instance.comboBox1.SelectedItem).Key;
                windowEvent = value;
                AppName = instance.textBox1.Text;
           
            }
            else
            {
                form.Close();
            }


        }
        public override PanelControl OnSelect()
        {

            return new WindowTrigger(this);
        }

        public override void OnInit(Event value)
        {
            TimedProcessWatcher process = new TimedProcessWatcher(1.0);
            process.ProcessStarted += (s, e) =>
            {
                ProcessHelper(e.Process, 1, value);
            };
            process.ProcessStopped += (s, e) =>

            {
                ProcessHelper(e.Process, 0, value);

            };
            process.Start();

        }
    }
}
