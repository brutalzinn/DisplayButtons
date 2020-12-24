using BackendAPI.Objects;
using DisplayButtons.BackendAPI.Objects;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using DisplayButtons.Forms.EventSystem;
using DisplayButtons.Forms.EventSystem.Controls;
using DisplayButtons.Forms.EventSystem.Controls.triggers;
using DisplayButtons.Helpers;
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
        public class GlobalAbstractDeckAction
        {
            public string Text { get; set; }
            public AbstractDeckAction Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
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

        public class FactoryEventControl
        {
            public string Text { get; set; }
            public Event Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public void CreateEvent()
        {




        }
        //type = 0 is trigger
        // type = 1 is action
        public void ToExecuteFormGeneral(int type)
        {
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;

            form.FillComboBox(type);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (type == 0)
                {
                    form.global_panelControl.SaveConfig();
                    new FactoryForms().SaveButtonTrigger(form.global_panelControl.getClassImplementTrigger);
                }
                if(type == 1)
                {
                    form.global_panelControl.SaveConfig();
                    new FactoryForms().SaveButtonAction(form.global_panelControl.getClassImplementAction);

                }
            }
            else
            {
                form.Close();
            }


        }

    }
}
