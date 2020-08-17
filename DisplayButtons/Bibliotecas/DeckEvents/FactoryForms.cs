using DisplayButtons.Backend.Objects.Implementation;
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

       
        public void SaveButton( AbstractTrigger type)
        {
            
             trigger_user_control.Instance.Add(type);
        }
        public void SaveButton(AbstractAction type)
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

       
    }
}
