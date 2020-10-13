using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Backend
{
    public class DynamicFolderExtension : DynamicDeckFolder
    {
       
        public void KeyGlobalValueHelper()
        {

            var keyInfo = new KeyInfoGlobal(KeyGlobalValue.ModifierKeys, KeyGlobalValue.Keys);
            dynamic form = Activator.CreateInstance(AbstractDeckAction.FindType("DisplayButtons.Forms.ActionHelperForms.FolderGlobalHotKey")) as Form;

            var execAction = new DynamicDeckFolder() as DynamicDeckFolder;
            execAction.KeyGlobalValue = KeyGlobalValue;
            form.ModifiableAction = execAction;

            if (form.ShowDialog() == DialogResult.OK)
            {
                KeyGlobalValue = form.ModifiableAction.KeyGlobalValue;
                GlobalHotKeys.refreshFolder(this);
            }
            else
            {
                KeyGlobalValue = keyInfo;
            }
        }
    }
}
