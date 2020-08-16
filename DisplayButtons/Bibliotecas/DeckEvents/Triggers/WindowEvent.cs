using DisplayButtons.Forms.EventSystem.Controls.actions;
using DisplayButtons.Forms.EventSystem.Controls.triggers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents.Actions
{
    public class WindowEvent : AbstractTrigger
    {
      
        public override string GetActionName()
        {
            return "Window Event";
        }

        public override Type GetActionType()
        {
            throw new NotImplementedException();
        }

        public override void OnExecute()
        {
            throw new NotImplementedException();
        }

     
        public override UserControl OnSelect()
        {

            return new WindowTrigger();
        }

       
    }
}
