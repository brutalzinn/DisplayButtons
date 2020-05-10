using ButtonDeck.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects
{

    [Serializable]
    public class DeckImplementation
    {


   

        public class packages
        {
            public static List<packages> plugins_list = new List<packages>();

            private string name;
            private string name_space;

            private string entry_point;

     

            public packages(string name, string name_space, string entry_point)
            {
                this.name = name;
                this.name_space = name_space;

                this.entry_point = entry_point;
                MainForm.button_creator(name,name_space,entry_point);
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public string Name_space
            {
                get { return name_space; }
                set { name_space = value; }
            }

            public string Entry_point
            {
                get { return entry_point; }
                set { entry_point = value; }
            }
            public static string getScript(string name_spaceX)
            {
                return plugins_list.Select(a => a.Name_space == name_spaceX).ToString();



            }



        }



















    }
}
