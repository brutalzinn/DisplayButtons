﻿using ButtonDeck.Backend.Utils;
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

      
        public static bool status { get; set; }
        public static int calc  { get; set; } //ApplicationSettingsManager.Settings.coluna * ApplicationSettingsManager.Settings.linha;
    public  static int linha  { get; set;}

        public static int ConsoleOpenned { get; set; }
        public static bool can_refresh { get; set; } = false;
        public static int coluna { get; set; }
        public static string updateurl = "http://127.0.0.1/update.xml";
    }
}
