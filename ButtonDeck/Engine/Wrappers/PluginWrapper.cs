using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck.Engine.Wrappers
{
    [MoonSharpUserData]
    class PluginWrapper
    {


        public static Dictionary<string, string> users = new Dictionary<string, string>();

        public static void Set(string key, string value)
        {
            if (users.ContainsKey(key))
            {
                users[key] = value;
            }
            else
            {
                users.Add(key, value);
            }
        }

        public static string Get(string key)
        {
            string result = null;

            if (users.ContainsKey(key))
            {
                result = users[key];
            }

            return result;
        }

    
           
           



          
    }
}
