using ButtonDeck.Backend.Utils;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects.Implementation.DeckActions.General
{
    public class FolderAddAction : AbstractDeckAction
    {
      

   

        public override bool IsPlugin()
        {
            ScribeBot.Scripter.Environment.Globals["name_space"] = name_space;
            return true;
        }



        //     public static string script { get; set; } = "";
        public static string script { get; set; } = "";
        public  string script_to_form { get; set; } = "";
        public static string name_space { get; set; } = "";
        public static string DeckActionCategory_string { get; set; } = "Deck";
        [ActionPropertyInclude]
        [ActionPropertyDescription("name")]
        public static string name { get; set; } = "teste";
        [ActionPropertyInclude]
        [ActionPropertyDescription("To Execute")]
        public string ToExecute { get; set; } = "";
        [ActionPropertyInclude]
        [ActionPropertyDescription("dictionary")]
        public static string dictionary_name { get; set; } = "";
       public static dynamic form;
        [MoonSharpUserData]

        public class formcontrol
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

            public static void setFormControl(string name, string value, string type)
            {

                switch (type)
                {
                    case "textbox":
                      TextBox txt = new TextBox();
                        txt.Text = value;
                        txt.Name = name;
                        //       FolderAddAction.form.Controls.Add(txt);

                        form.Controls.Add(txt);

                        break;
                    case "label":
                        Label labeled = new Label();
                        labeled.Text = value;
                        labeled.Name = name;
                        //       FolderAddAction.form.Controls.Add(txt);

                        form.Controls.Add(labeled);

                        break;



                }


            }
        }
        public void ToExecuteHelper()
        {
            ScribeBot.Scripter.Execute(script, false);

            //          ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);
            var originalToExec = new String(ToExecute.ToCharArray());
             form = Activator.CreateInstance(FindType("ButtonDeck.Forms.ActionHelperForms.ActionPlugin")) as Form;
           
            ScribeBot.Scripter.Environment.Globals["formdesign"] = typeof(formcontrol);
            object functiontable = ScribeBot.Scripter.Environment.Globals["form_menu"];
            ScribeBot.Scripter.Environment.Call(functiontable);
          //  var execAction = CloneAction() as FolderAddAction;
     
        //    execAction.ToExecute = ToExecute;

         //   form.ModifiableAction = execAction;


            form.scripter = script;
            if (form.ShowDialog() == DialogResult.OK)
            {

                ScribeBot.Scripter.Execute(script, false);

                object functioncall = ScribeBot.Scripter.Environment.Globals["menu_ok"];
                ScribeBot.Scripter.Environment.Call(functioncall);
                // ToExecute = form.ModifiableAction.toExecute;  
                // oExecute = form.ModifiableAction;
            }
            else
        {
                ToExecute = originalToExec;
            }
        }

        public override AbstractDeckAction CloneAction()
        {
            Debug.WriteLine("CHEGOU A CHAMAR O NAMESPACE" + name_space);
        //    DynValue obj = UserData.Create(name_space);
            //  ScribeBot.Scripter.Execute(script, false);
       //     ScribeBot.Scripter.Environment.Globals.Set("name_space", obj);

         
            return new FolderAddAction();
        }
      

        public override DeckActionCategory GetActionCategory()
        {

            DeckActionCategory animal = (DeckActionCategory)Enum.Parse(typeof(DeckActionCategory), DeckActionCategory_string);
            return animal;

        }

        public override string GetActionName()
        {

            return name;
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {
 Debug.WriteLine("CONTINENTE");
    //        ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);


            ScribeBot.Scripter.Execute(script,false);
           object functiontable = ScribeBot.Scripter.Environment.Globals["ButtonDown"];
          ScribeBot.Scripter.Environment.Call(functiontable);
            //  ScribeBot.Scripter.Environment.Call(DynValue.NewString("buttondown"));
            
          
        }
      
        public override void OnButtonUp(DeckDevice deckDevice)
        {
        //    ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);

            //    ScribeBot.Scripter.Execute(script);
            //    DynValue luaFactFunction = ScribeBot.Scripter.Environment.Globals.Get("ButtonDown");
            ScribeBot.Scripter.Execute(script, false);
            object functiontable = ScribeBot.Scripter.Environment.Globals["ButtonUP"];
            ScribeBot.Scripter.Environment.Call(functiontable);
            //    DynValue res = ScribeBot.Scripter.Environment.Call(luaFactFunction);
            //   ScribeBot.Scripter.Execute(res.tos);
        }
    }
}
