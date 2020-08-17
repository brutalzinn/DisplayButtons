using DisplayButtons.Forms.EventSystem;
using DisplayButtons.Forms.EventSystem.Controls;
using DisplayButtons.Forms.EventSystem.Controls.EventCreator.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    public class FactoryForms
    {

       
        public void SaveButton( AbstractTrigger type,params object[] values)
        {
            
             trigger_user_control.Instance.Add(values[0].ToString(), type);
        }
        public void SaveButton(AbstractAction type, params object[] values)
        {
            action_user_control.Instance.Add(values[0].ToString(), type);
        }
        public class AbstracActionControl
        {
            public string Text { get; set; }
            public AbstractAction Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public class AbstractTriggerControl
        {
            public string Text { get; set; }
            public AbstractTrigger Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
