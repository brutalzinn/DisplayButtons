using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.Implementation;
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

namespace DisplayButtons.Backend.Objects
{
    public class ProfileVoidHelper
    {
 
        public class GlobalPerfilBox

        {
            public string Text { get; set; }
            public Profile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

        }
        public void UndonePerfilSelector()
        {

            MainForm.Instance.Invoke(new Action(() =>
            {

                MainForm.Instance.ClearMatriz();

            }));

        }


        public void AddPerfil()
        {

            dynamic form = Activator.CreateInstance(UsbMode.FindType("DisplayButtons.Forms.PerfilEditor")) as Form;

            if (form.ShowDialog() == DialogResult.OK)
            {
                Profile teste = new Profile();
                teste.Name = form.textBox1.Text;
                teste.Mainfolder = new DynamicDeckFolder();
                //  teste.Currentfolder = teste.Mainfolder;
                if (DevicePersistManager.PersistedDevices.Count == 0)
                {
                    foreach (var device_con in DevicePersistManager.DeckDevicesFromConnection)
                    {
                        device_con.Value.profiles.Add(teste);
                    }
                }
                else
                {
                    foreach (var device in DevicePersistManager.PersistedDevices.ToList())
                    {
                        device.profiles.Add(teste);
                    }

                }
                MainForm.Instance.FillPerfil();

            }
            else
            {
                form.Close();
            }

        }

    }
    public static class ProfileStaticHelper
    {
        public static Profile DeviceSelected { get; set; }
        public static void var_dump(object obj)
        {
            Debug.WriteLine("{0,-18} {1}", "Name", "Value");
            string ln = @"-------------------------------------  
               ----------------------------";
            Debug.WriteLine(ln);

            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                try
                {
                    Debug.WriteLine("{0,-18} {1}",
                      props[i].Name, props[i].GetValue(obj, null));
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);  
                }
            }
            //   Debug.WriteLine();
        }


    
        public static Profile SelectPerfilByName(string profilename)
        {
            Profile result = null;
            foreach (var device in DevicePersistManager.PersistedDevices)
            {

                result =  device.profiles.Where(e => e.Name == profilename).FirstOrDefault();
            }
            return result;
        }
        public static void SelectCurrentDevicePerfil(Profile profile)
        {
            if (MainForm.Instance.CurrentDevice != null)
            {
                MainForm.Instance.Invoke(new Action(() =>
                {

                    var con = MainForm.Instance.CurrentDevice.GetConnection();
                SelectPerfilMatriz(profile);
                
                DevicePersistManager.DeckDevicesFromConnection.FirstOrDefault().Value.CurrentProfile = profile;
                MainForm.Instance.MatrizGenerator(profile);
                  
                var Matriz = new MatrizPacket(profile);
                con.SendPacket(Matriz);

                MainForm.Instance.CurrentDevice.CurrentProfile.Currentfolder = profile.Mainfolder;
               
        
                MainForm.Instance.ChangeToDevice(MainForm.Instance.CurrentDevice);
                DeviceSelected = profile;
                ApplicationSettingsManager.Settings.CurrentProfile = profile.Name;
  MainForm.Instance.RefreshAllButtons(true);
                }));
            }
        }
        public static bool CheckProfileMatriz(Profile profile)
        {
            if (profile.Matriz != null)
            {
                return true;
            }
            else
            {
              return  false;
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
        public static void SelectDevicePerfil(DeckDevice device,Profile profile)
        {
            device.CurrentProfile = profile;
          
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
        public static void SetupPerfil()
        {
       
          
                foreach (var device in DevicePersistManager.PersistedDevices.ToList())
                {
                    if (device.profiles.Count == 0)
                    {

                        Profile new_folder = new Profile();
                        new_folder.Mainfolder = new DynamicDeckFolder();
                        new_folder.Name = "DEFAULT";

                        device.profiles.Add(new_folder);
                        SelectCurrentDevicePerfil(new_folder);

                        MainForm.Instance.FillPerfil();
                    }


                }

            

        }

   
     public static void RemovePerfil(Profile perfil)
    {
        foreach (var device in DevicePersistManager.PersistedDevices)
        {
            if (device.profiles.Count > 1)
            { 
                   
 device.profiles.Remove(perfil);
                    SelectCurrentDevicePerfil(device.profiles.Last());
                   
                MainForm.Instance.FillPerfil();
            }


        }
    }
    


    }
    }








