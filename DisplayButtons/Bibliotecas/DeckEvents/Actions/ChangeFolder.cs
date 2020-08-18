using DisplayButtons.Backend.Objects;
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

        public void ToExecuteHelper()
        {
            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;


            //  form.comboBox.Visible = false;
            form.Controls.Remove(form.comboBox);
            form.panel1.Controls.Clear();
            form.panel1.Controls.Add(new WindowAction(this));

            if (form.ShowDialog() == DialogResult.OK)
            {
                folder = form.textBox1.Text;
                new FactoryForms().SaveButtonAction(this);
            }
            else
            {
                form.Close();
            }


        }

        public override void OnExecute()
        {
            throw new NotImplementedException();
        }

     
        public override PanelControl OnSelect()
        {

            return new WindowAction(this);
        }

       
    }
}
