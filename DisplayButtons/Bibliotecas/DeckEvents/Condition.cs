using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    class Condition
    {
        public bool timer_interval { get; set; }
        public bool timer_extact { get; set; }
        public bool timer_after { get; set; }

        public string lua_path { get; set; }
        public Condition(string timer_interval_a, string timer_interval_b,string timer_exact)
        {


        }


        public bool CheckCondition()
        {

            return true;
        }
    }
}
