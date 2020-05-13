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
            return true;
        }
  

        [ActionPropertyInclude]
        [ActionPropertyDescription("name")]
          public static string name { get; set; } = "teste";
        public static string script { get; set; } = "";
        public static string DeckActionCategory_string { get; set; } = "Deck";
        [ActionPropertyInclude]
        [ActionPropertyDescription("To Execute")]
        public string ToExecute { get; set; } = "";
        public string list_vars { get; set; }
        public string dic_get
        {


            get
            { // serialize
                if (LIST.Get(list_vars) == null) return null;
                return LIST.Get(list_vars);
            }
            set
            { // deserialize
                if (value == null)
                {
                 //   LIST.Get(list_vars) = null;
                }
                else
                {
              //      LIST.Set(list_vars) = value;

                }

            }
        }
        public class LIST
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
        
        public void ToExecuteHelper()
        {
            ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);
            var originalToExec = new String(ToExecute.ToCharArray());
            dynamic form = Activator.CreateInstance(FindType("ButtonDeck.Forms.ActionHelperForms.ActionPlugin")) as Form;
            var execAction = CloneAction() as FolderAddAction;
            execAction.ToExecute = ToExecute;
            form.ModifiableAction = execAction;
            
            if (form.ShowDialog() == DialogResult.OK)
            {


              ToExecute = form.ModifiableAction.toExecute;  
               // oExecute = form.ModifiableAction;
            }
            else
        {
                ToExecute = originalToExec;
            }
        }

        public override AbstractDeckAction CloneAction()
        {
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
            ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);


            ScribeBot.Scripter.Execute(script,false);
           object functiontable = ScribeBot.Scripter.Environment.Globals["ButtonDown"];
          ScribeBot.Scripter.Environment.Call(functiontable);
            //  ScribeBot.Scripter.Environment.Call(DynValue.NewString("buttondown"));
            
          
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
            ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);

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
