using DisplayButtons.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
   public  class Config
    {
        public static int mode { get; set; }
        public static ServerThread ServerThread { get; set; }
        public static ClientThread ClientThread { get; set; }
    }
}
