using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Engine.Wrappers
{
    [MoonSharpUserData]
    class PluginWrapper
    {

        public static Dictionary<string, string> myLists =
        new Dictionary<string, string>();
        public void  setPlayerString(string key, string value)
        {
            myLists.Add(key, value);

        }
        
            public static string getPlayerString(string key, bool keyorpair)
        {
            try
            {

           
            foreach (KeyValuePair<string, string> keyparrr in myLists)
            {
              //  Console.WriteLine("Key: {0}, Value: {1}",
            
              if(keyorpair == true)
                {
  if(key == keyparrr.Key)
                {


                  return  keyparrr.Value;

                }


                }
                else
                {
                    if (key == keyparrr.Value)
                    {


                        return keyparrr.Key;

                    }


                }
            }
            }



            catch (Exception e)
            {

                return "";
            }
            return "";
        }
    }
}
