using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using DisplayButtons.Forms.EventSystem;
using DisplayButtons.Forms.EventSystem.Controls;
using DisplayButtons.Forms.EventSystem.Controls.triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    public class FactoryForms
    {

       
        public void SaveButtonTrigger( AbstractTrigger type)
        {
            
             trigger_user_control.Instance.Add(type);
        }
        public void SaveButtonAction(AbstractAction type)
        {
            action_user_control.Instance.Add(type);
        }
        public class GlobalControl
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public class FactoryTriggerControl
        {
            public string Text { get; set; }
            public AbstractTrigger Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public class FactoryActionControl
        {
            public string Text { get; set; }
            public AbstractAction Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public void ToExecuteFormGeneral()
        {
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;
     
            
            if (form.ShowDialog() == DialogResult.OK)
            {

                form.global_trigger.SaveConfig();
                new FactoryForms().SaveButtonTrigger(form.global_trigger.getClassImplementTrigger);

            }
            else
            {
                form.Close();
            }


        }

    }
}
