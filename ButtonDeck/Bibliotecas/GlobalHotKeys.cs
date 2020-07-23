using ButtonDeck.Backend.Objects.Implementation;
using ButtonDeck.Forms;
using NHotkey;
using NHotkey.WindowsForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace ButtonDeck.Bibliotecas
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
     

        public static int VirtualKeyFromKeys(Keys[] keys)
        {
            int result = 0;

            foreach (var k in keys)
            {

                result += (int)k;
            }

            return result;
        }

        public void GarbageHotKeyCollector(DynamicDeckFolder folder)
        {
            

                
                string unique_id = folder.GetParent().GetItemIndex(folder) + "";
                   HotkeyManager.Current.Remove(unique_id) ;
            
        }
      
        public void refreshFolder(DynamicDeckFolder folder)
        {
            string value1 = string.Join("+", folder.KeyGlobalValue.ModifierKeys.Where(c => c != Keys.None).Select(c => c.ToString()).OrderBy(c => c));
            string value2 = string.Join("+", folder.KeyGlobalValue.Keys.Where(c => !(c == Keys.ShiftKey || c == Keys.ControlKey || c == Keys.Menu)));
            string result = string.IsNullOrEmpty(value1) ? value2 : string.IsNullOrEmpty(value2) ? value1 : (string.Join("+", value1, value2));
            result = result.Replace("Control", "Ctrl");

            System.Windows.Forms.Keys retval = System.Windows.Forms.Keys.None;

            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
                    retval = (System.Windows.Forms.Keys)kc.ConvertFromInvariantString(result);

                    string unique_id = folder.GetParent().GetItemIndex(folder) + "";
                    var handlerEvent = new ActionsFolderButtons(folder);
                  
                    HotkeyManager.Current.AddOrReplace(unique_id, retval, handlerEvent.MyEventHandler);

                    
           
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
