using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
