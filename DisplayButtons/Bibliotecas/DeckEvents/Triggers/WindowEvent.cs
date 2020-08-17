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

            form.Show();
    //  form.comboBox.Visible = false;
            form.Controls.Remove(form.comboBox);
           form.panel1.Controls.Clear();
            form.panel1.Controls.Add( new WindowTrigger(this));
           
           
       //     execAction.keyBackFolder = ApplicationSettingsManager.Settings.keyBackFolder;
      
           /// form.ModifiableAction = execAction;

            if (form.DialogResult == DialogResult.OK)
            {
              

            }
            else
            {
                
            }


        }
        public override UserControl OnSelect()
        {

            return new WindowTrigger(this);
        }

       
    }
}
