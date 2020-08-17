using DisplayButtons.Backend.Objects;
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
        public string AppName;
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

        public void ToExecuteHelper()
        {
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;

            var execAction = new WindowEvent();
       //     execAction.keyBackFolder = ApplicationSettingsManager.Settings.keyBackFolder;
      
           /// form.ModifiableAction = execAction;

            if (form.ShowDialog() == DialogResult.OK)
            {
            

            }
            else
            {
                
            }


        }
        public override UserControl OnSelect()
        {

            return new WindowTrigger();
        }

       
    }
}
