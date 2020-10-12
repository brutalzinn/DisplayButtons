using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Forms;
using DisplayButtons.Misc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;


namespace DisplayButtons.Bibliotecas.Helpers.ObjectsHelpers
{
    public static class ProfileTestDeckHelper
    {
        public static Profile DeviceSelected { get; set; }
        public static void SelectCurrentDevicePerfil(Profile profile, DeckDevice device)
        {
          
                MainForm.Instance.Invoke(new Action(() =>
                {

                    
                    SelectPerfilMatriz(profile);
device.CurrentProfile = profile;
                    MainForm.Instance.MatrizGenerator(profile);

                  
                    MainForm.Instance.CurrentDevice.CurrentProfile.Currentfolder = profile.Mainfolder;


                    MainForm.Instance.ChangeToDevice(MainForm.Instance.CurrentDevice);
                    
                    
           DeviceSelected = profile;
                    ApplicationSettingsManager.Settings.CurrentProfile = profile.Name;
                   MainForm.Instance.RefreshAllButtons(true);
                }));
            
        }
        public static bool CheckProfileMatriz(Profile profile)
        {
            if (profile.Matriz != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void SelectPerfilMatriz(Profile profile)
        {
            if (CheckProfileMatriz(profile) == false)
            {

                MatrizObject matriz_new = new MatrizObject(3, 5);
                profile.Matriz = matriz_new;

            }


        }
        public static void SelectDevicePerfil(DeckDevice device, Profile profile)
        {
            device.CurrentProfile = profile;

        }
        public static Profile getCurrentPerfilComboBox(this ComboBox cbo)
        {
            Profile CurrentPerfil = null;
            MainForm.Instance.Invoke(new Action(() =>
            {

                CurrentPerfil = ((ProfileVoidHelper.GlobalPerfilBox)cbo.SelectedItem).Value;

            }));
            return CurrentPerfil;

        }
        public static void SelectItemByValue(this ComboBox cbo, Profile profile)
        {

            if (profile != null)
            {
                foreach (ProfileVoidHelper.GlobalPerfilBox item in cbo.Items)
                {

                    if (item.Value.Equals(profile))
                    {
                        cbo.SelectedItem = item;
                        break;
                    }


                }
            }
        }
        public static void SetupPerfil(DeckDevice device)
        {


                

                    Profile new_folder = new Profile();
                    new_folder.Mainfolder = new DynamicDeckFolder();
                    new_folder.Name = "TESTE";

                    device.profiles.Add(new_folder);

              ///      SelectCurrentDevicePerfil(new_folder,device);




                    MainForm.Instance.FillPerfil();
                    MainForm.Instance.Invoke(new Action(() =>
                    {

                        SelectItemByValue(MainForm.Instance.perfilselector, new_folder);
                    }));
                


            }



        
    }
}
