using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Bibliotecas.Helpers;
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
using WebSocketSharp;

namespace DisplayButtons.Bibliotecas
{
    class GlobalHotKeys
    {
     
     

    public static void UpdateAllFoldersHotkeys()
        {

      DeckHelpers.ListFolders(MainForm.Instance.CurrentDevice.CurrentProfile.Mainfolder as DynamicDeckFolder).ForEach(e => {
          if (!e.UniqueID.IsNullOrEmpty())
          {
              refreshFolder(e);
          }

            });

       
                
                
       }
        public static void RegisterUniqueId(DynamicDeckFolder folder)
        {
            if (folder != null)
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
                folder.UniqueID = ShortId.Generate(true, false, 12);
#pragma warning restore CS0618 // O tipo ou membro é obsoleto

        }
        public static void RemoveHotKey(DynamicDeckFolder folder)
        {

            if(folder != null) { 

           
HotkeyManager.Current.Remove(folder.UniqueID) ;

            }
                   
            
        }
      
        public static void refreshFolder(DynamicDeckFolder folder)
        {
            string value1 = string.Join("+", folder.KeyGlobalValue.ModifierKeys.Where(c => c != Keys.None).Select(c => c.ToString()).OrderBy(c => c));
            string value2 = string.Join("+", folder.KeyGlobalValue.Keys.Where(c => !(c == Keys.ShiftKey || c == Keys.ControlKey || c == Keys.Menu)));
            string result = string.IsNullOrEmpty(value1) ? value2 : string.IsNullOrEmpty(value2) ? value1 : (string.Join("+", value1, value2));
            result = result.Replace("Control", "Ctrl");

            System.Windows.Forms.Keys retval = System.Windows.Forms.Keys.None;
            if (!folder.UniqueID.IsNullOrEmpty())
          
            if (!result.IsNullOrEmpty())
            {
                try
                {
                    System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
                    retval = (System.Windows.Forms.Keys)kc.ConvertFromInvariantString(result);

                  
                      var handlerEvent = new ActionsFolderButtons(folder);
                  
                    HotkeyManager.Current.AddOrReplace(folder.UniqueID, retval, handlerEvent.MyEventHandler);

                    
           
                }
                catch (Exception)
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

                Debug.WriteLine("Trocando para pasta: "+  FolderPrincipal.GetDeckDefaultLayer.Deckname + " Atalho: "  + e.Name);
                MainForm.Instance.CurrentDevice.CurrentProfile.Currentfolder = FolderPrincipal;
                MainForm.Instance.RefreshAllButtons(true);
         e.Handled = true;
        }
        }
        


    }
}
