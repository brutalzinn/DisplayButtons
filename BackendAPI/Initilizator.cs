using Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendAPI
{
    public class Initilizator
    {

        public static int mode { get; set; }
        public static ServerThread ServerThread { get; set; }
        public static ClientThread ClientThread { get; set; }
        public Initilizator(int _mode)
        {
            mode = _mode;

        }
    }
}
