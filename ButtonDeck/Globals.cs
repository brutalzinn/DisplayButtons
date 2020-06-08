using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck
{

    class Globals
    {


        public static int calc = ApplicationSettingsManager.Settings.coluna * ApplicationSettingsManager.Settings.linha;
        public  static int linha  { get; set;}
           
        public static int coluna { get; set; }
    }
}
