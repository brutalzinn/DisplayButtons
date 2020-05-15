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

        public static Dictionary<string, string> myLists =
        new Dictionary<string, string>();
        public void  setPlayerString(string key, string value)
        {
            myLists.Add(key, value);

        }
        public static Forms.ActionHelperForms.ActionPlugin MainWindow { get; private set; }
        public void setFormControl(string name, string value, int posX, int posY, string type)
        {

            switch (type)
            {
                case "textbox":
                   // Forms.ActionHelperForms.ActionPlugin teste = new Forms.ActionHelperForms.ActionPlugin();
                    System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();

                    txt.Text = value;
                    MainWindow?.Invoke(new Action(() =>
                    {
                        MainWindow.Controls.Add(txt);

                    }));
              


                        break;



            }


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
