using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;


namespace DisplayButtons
{
   public class Texts
    {

       

        public static CultureInfo cultereinfo = new CultureInfo("pt-BR");
        public static Assembly a = Assembly.Load("DisplayButtons");
        public static ResourceManager rm = new ResourceManager("DisplayButtons.Langs.langres", a);
     
        public static void initilizeLang()
        {

            switch (ApplicationSettingsManager.Settings.Language){


                case "pt-BR":
                    cultereinfo = new CultureInfo("pt-BR");
                    Globals.updateurl = $"http://{Globals.domain}/pt/{Globals.updatefilename}";
                    break;

                case "en-US":
            cultereinfo = new CultureInfo("en-US");
                    Globals.updateurl = $"http://{Globals.domain}/en/{Globals.updatefilename}";
                    break;
                case "es-EN":
                    cultereinfo = new CultureInfo("es-UN");
                    break;
            }

            Globals.events.Trigger("languagechanged");

        }
    }
}
