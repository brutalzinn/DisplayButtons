using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck
{
    [Serializable]
    class Globals
    {

        public  Forms.MainForm launcher_principal;

        public  static int linha  { get; set;}
           
        public static int coluna { get; set; }
    }
}
