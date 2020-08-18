using DisplayButtons.Backend.Objects;
using DisplayButtons.Forms.EventSystem.Controls.actions;
using DisplayButtons.Forms.EventSystem.Controls.triggers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DisplayButtons.Forms;
using System.Linq;

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
        public override AbstractTrigger CloneAction()
        {
            return new WindowEvent();
        }
        public void ToExecuteHelper()
        {
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;

        
            // form.comboBox.SelectedItem = GetActionName();
          form.Controls.Remove(form.comboBox);
           form.panel1.Controls.Clear();
            form.UpdateForm(new WindowTrigger(this)) ;

            if (form.ShowDialog() == DialogResult.OK)
            {

                // TextBox teste = form.global_panelControl.getControl.Controls.Find("textBox1", false).FirstOrDefault() as TextBox;
                TextBox control = form.global_panelControl.getControl.Controls.Find("textBox1", true).FirstOrDefault() as TextBox;
                if (control != null)
                {
                    AppName = control.Text;
                }
              //  form.global_panelControl.SaveConfig();
                new FactoryForms().SaveButtonTrigger(this);
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

       
    }
}
