using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    [MoonSharpUserData]
    public class Condition
    {
        [MoonSharpUserData]
        public  class Events
        {
            private Condition instance;
            public Events(Condition param)
            {

                instance = param;
            }
            public void Continue()
            {
               instance.canContinue = true;
                Debug.WriteLine("LUA CONTINUE");
            }
            public void Cancel()
            {
                instance.canContinue = false;
                Debug.WriteLine("LUA CANCEL");
            }
        }
        [XmlIgnore]
        public bool canContinue { get; set; }
        public bool timer_interval { get; set; }
        public bool timer_now { get; set; }
        public bool timer_after { get; set; }
        public bool timer_before { get; set; }
        public bool timer_none { get; set; }
        public string lua_path { get; set; }

        public string timer_interval_start { get; set; }

        public string timer_interval_end { get; set; }

        public string timer_interval_now { get; set; }
        public string timer_interval_after { get; set; }
        public string timer_interval_before { get; set; }
    
        public Condition(){


            }
        public Condition(string timer_interval_a, string timer_interval_b,string timer_exact)
        {


        }
        public bool CheckTimerNow()
        {

            bool result = false;
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan start = TimeSpan.Parse(timer_interval_now);
            if (now== start)
            {
                result = true;
            }


            return result;

        }
        public bool CheckLuaScript()
        {
            bool result= false;
            string script = File.ReadAllText(lua_path);
            ScribeBot.Scripter.Environment.Globals["events"] = new Events(this);
            try
            {
            ScribeBot.Scripter.Execute(script, true);

            }
            finally
            {
                result = canContinue;
            }

            return result;
        }
        public bool CheckTimerAfter()
        {

            bool result = false;
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan start = TimeSpan.Parse(timer_interval_after);
            if (now >= start)
            {
                result = true;
            }


            return result;

        }
        public bool CheckTimerBefore()
        {

            bool result = false;
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan start = TimeSpan.Parse(timer_interval_before);
            if (now <= start)
            {
                result = true;
            }


            return result;

        }
        public bool CheckTimerInterval()
        {
            bool result= false;
            TimeSpan start = TimeSpan.Parse(timer_interval_start); // 10 PM
            TimeSpan end = TimeSpan.Parse(timer_interval_end);   // 2 AM
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (start <= end)
            {
                // start and stop times are in the same day
                if (now >= start && now <= end)
                {
                    result = true;
                }
            }
            else
            {
                // start and stop times are in different days
                if (now >= start || now <= end)
                {
                    result =  true;
                }
            }
            return result;

        }

        public bool CheckCondition()
        {
            bool result = false;
            if (timer_interval)
            {
result = CheckTimerInterval();

            }
            if (timer_now)
            {

                result = CheckTimerNow();
            }
            if (timer_after)
            {

                result = CheckTimerAfter();
            }
            if (timer_before)
            {

                result = CheckTimerBefore();
            }
            if (!String.IsNullOrEmpty(lua_path))
            {

                result = CheckLuaScript();
            }
            if (timer_none)
            {

                result = true;
            }
            return result;


        }
    }
}
