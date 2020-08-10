using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;


namespace ButtonDeck
{
    class Texts
    {

        private static Dictionary<string, string> Text = new Dictionary<string, string>
        {
            {"DEVICENAME",                     "Nome do dispositivo"},
            {"THEME",                           "Tema"},
            {"IFTTMAKERKEYTEXT",                "IFTT Maker Chave"},
            {"BUTTONSAVE",                      "Salvar"},
            {"BUTTONCANCEL",                    "Cancelar"},
            {"BUTTONRELOADALL",                 "Recarregar tudo"},
            {"BUTTONRELOADEXTERNBUTTON",        "Recarregar botões externos"},
            {"BUTTONRELOADBUTTONS",             "Recarregar botões"},
            {"BUTTONOPENCONSOLE",               "Abrir console"},
            {"CURRENTPROGRESS",                 "Progresso por arquivo: {0}%  |  {1} kb/s"},
            {"CHECKCOMPLETE",                   "Todo arquivo foi checado completamente."},
            {"DOWNLOADCOMPLETE",                "Todos os arquivos requeridos foram baixados completamente."},
            {"DOWNLOADSPEED",                   "{0} kb/s"},
            {"FILESEXTRACT",                    "{0} Extraindo arquivo.. de {1}"}
        };
        public static string GetText(string Key, params object[] Arguments)
        {
            foreach (var currentItem in Text)
            {
                if (currentItem.Key == Key)
                {
                    return string.Format(currentItem.Value, Arguments);
                }
            }

            return null;
        }
        public static CultureInfo cultereinfo = new CultureInfo("pt-BR");
        public static Assembly a = Assembly.Load("DisplayButtons");
        public static ResourceManager rm = new ResourceManager("DisplayButtons.Langs.langres", a);
        public static void initilizeLang()
        {

            switch (ApplicationSettingsManager.Settings.Language){


                case "pt-BR":
                    cultereinfo = new CultureInfo("pt-BR");
                    Globals.updateurl = $"http://{Globals.domain}/update/pt/{Globals.updatefilename}";
                    break;

                case "en-US":
            cultereinfo = new CultureInfo("en-US");
                    Globals.updateurl = $"http://{Globals.domain}/update/en/{Globals.updatefilename}";
                    break;
                case "es-EN":
                    cultereinfo = new CultureInfo("es-UN");
                    break;
            }
     
          

        }
    }
}
