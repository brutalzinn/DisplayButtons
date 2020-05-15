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


<<<<<<< HEAD
        }
        public static Forms.ActionHelperForms.ActionPlugin MainWindow { get; private set; }
        public void setFormControl(string name, string value, int posX, int posY, string type)
        {
=======
        public static Dictionary<string, string> users = new Dictionary<string, string>();
>>>>>>> ac0a79332a789253b59848561cc0d72763a4c710

        public static void Set(string key, string value)
        {
            if (users.ContainsKey(key))
            {
<<<<<<< HEAD
                case "textbox":
                   // Forms.ActionHelperForms.ActionPlugin teste = new Forms.ActionHelperForms.ActionPlugin();
                    System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();

                    txt.Text = value;
                    MainWindow?.Invoke(new Action(() =>
                    {
                        MainWindow.Controls.Add(txt);

                    }));
              


                        break;



=======
                users[key] = value;
            }
            else
            {
                users.Add(key, value);
>>>>>>> ac0a79332a789253b59848561cc0d72763a4c710
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
