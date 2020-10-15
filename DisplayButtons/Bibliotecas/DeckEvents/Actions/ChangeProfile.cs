using Backend.Objects;
using DisplayButtons.Backend.Objects;

using DisplayButtons.Forms;
using DisplayButtons.Forms.EventSystem.Controls.actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents.Actions
{
    public class ChangeProfile : AbstractAction
    {
        public string profile;
        public override string GetActionName()
        {
            return "Change to profile";
        }
        
        public void ToExecuteHelper()
        {
            dynamic form = Activator.CreateInstance(AbstractDeckAction.FindType("DisplayButtons.Forms.EventSystem.EventCreateNew")) as Form;


            //  form.comboBox.Visible = false;
            form.Controls.Remove(form.comboBox);
            var instance = new ChangeProfileAction(this);
            form.UpdateForm(instance, 1);

            if (form.ShowDialog() == DialogResult.OK)
            {
                profile =  ((ProfileVoidHelper.GlobalPerfilBox)instance.comboBox1.SelectedItem).Value.Name;
            
              //  folder = (DynamicDeckFolder)instance.comboBox1.SelectedItem;

            }
            else
            {
                form.Close();
            }


        }
        public override AbstractAction CloneAction()
        {
            return new ChangeFolder();
        }
        public override void OnExecute()
        {
           

            ProfileStaticHelper.SelectCurrentDevicePerfil( ProfileStaticHelper.SelectPerfilByName(profile));
            MainForm.Instance.Invoke(new Action(() =>
            {

                ProfileStaticHelper.SelectItemByValue(MainForm.Instance.perfilselector, ProfileStaticHelper.SelectPerfilByName(profile));

            }));
         
        }


        public override PanelControl OnSelect()
        {

            return new ChangeProfileAction(this);
        }

        public override void OnInit()
        {
            throw new NotImplementedException();
        }
    }
}
