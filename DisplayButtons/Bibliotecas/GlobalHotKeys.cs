using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Forms;
using NHotkey;
using NHotkey.WindowsForms;
using shortid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace DisplayButtons.Bibliotecas
{
    class GlobalHotKeys
    {
        private static GlobalHotKeys instance;

        public static GlobalHotKeys Instance
        {
            get
            {
                return instance;
            }
        }
        public static List<DynamicDeckFolder> folder_form = new List<DynamicDeckFolder>();

        public GlobalHotKeys()
        {
            instance = this;
        }
     

    
        public void RegisterHotKeyCollector(DynamicDeckFolder folder)
        {
            folder.UniqueID = ShortId.Generate(true, false, 12);

        }
        public void GarbageHotKeyCollector(DynamicDeckFolder folder)
        {



            if (!String.IsNullOrEmpty(folder.UniqueID))
            {
HotkeyManager.Current.Remove(folder.UniqueID) ;

            }
                   
            
        }
      
        public void refreshFolder(DynamicDeckFolder folder)
        {
            string value1 = string.Join("+", folder.KeyGlobalValue.ModifierKeys.Where(c => c != Keys.None).Select(c => c.ToString()).OrderBy(c => c));
            string value2 = string.Join("+", folder.KeyGlobalValue.Keys.Where(c => !(c == Keys.ShiftKey || c == Keys.ControlKey || c == Keys.Menu)));
            string result = string.IsNullOrEmpty(value1) ? value2 : string.IsNullOrEmpty(value2) ? value1 : (string.Join("+", value1, value2));
            result = result.Replace("Control", "Ctrl");

            System.Windows.Forms.Keys retval = System.Windows.Forms.Keys.None;
            if (string.IsNullOrEmpty(folder.UniqueID))
            {
                return;
            }
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
                    retval = (System.Windows.Forms.Keys)kc.ConvertFromInvariantString(result);

                  
                      var handlerEvent = new ActionsFolderButtons(folder);
                  
                    HotkeyManager.Current.AddOrReplace(folder.UniqueID, retval, handlerEvent.MyEventHandler);

                    
           
                }
                catch (Exception ex)
                {
                    //Debug.(ex.ToString());
                }

            }
            
        }

   
     public class ActionsFolderButtons
        {
            public DynamicDeckFolder FolderPrincipal { get; set; }
            public ActionsFolderButtons(DynamicDeckFolder folder)
            {

                FolderPrincipal = folder;

            }

public void MyEventHandler(object sender, HotkeyEventArgs e)
        {

                Debug.WriteLine("Trocando para pasta: "+  FolderPrincipal.DeckName + " Atalho: "  + e.Name);
                MainForm.Instance.CurrentDevice.CurrentFolder = FolderPrincipal;
                MainForm.Instance.RefreshAllButtons(true);
         e.Handled = true;
        }
        }
        


    }
}
