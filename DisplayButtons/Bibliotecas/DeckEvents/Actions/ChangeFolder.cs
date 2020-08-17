using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Forms.EventSystem.Controls.actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents.Actions
{
    public class ChangeFolder : AbstractAction
    {

        public DynamicDeckFolder folder;
        public override string GetActionName()
        {
            return "Change to folder";
        }

        

        public override void OnExecute()
        {
            throw new NotImplementedException();
        }

     
        public override UserControl OnSelect()
        {

            return new WindowAction();
        }

       
    }
}
