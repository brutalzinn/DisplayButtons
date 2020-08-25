using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Forms;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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





    }
    public static class ProfileStaticHelper
    {
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
        public static void SelectCurrentDevicePerfil(Profile profile)
        {
            

          
           MainForm.Instance.CurrentDevice.CurrentProfile = profile;

           MainForm.Instance.CurrentDevice.CurrentProfile.Currentfolder = MainForm.Instance.CurrentDevice.CurrentProfile.Mainfolder;
        MainForm.Instance.ChangeToDevice(MainForm.Instance.CurrentDevice);
//

          MainForm.Instance.CurrentDevice.CheckCurrentFolder();    

    ApplicationSettingsManager.Settings.CurrentProfile = profile;
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
         
            foreach (var perfil in DevicePersistManager.PersistedDevices.ToList())
            {
              if(perfil.profiles.Count != 0)
                {
                    
                   
           
                  
                }
            }

        }
    }
}








