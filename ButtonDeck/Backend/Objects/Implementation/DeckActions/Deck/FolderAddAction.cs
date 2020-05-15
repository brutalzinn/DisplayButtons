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

       public static dynamic form;
        [MoonSharpUserData]
        class formcontrol
        {
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



                }


            }
        }
        public void ToExecuteHelper()
        {
          
            //          ScribeBot.Scripter.Environment.Globals["list"] = typeof(LIST);
            var originalToExec = new String(ToExecute.ToCharArray());
             form = Activator.CreateInstance(FindType("ButtonDeck.Forms.ActionHelperForms.ActionPlugin")) as Form;
            ScribeBot.Scripter.Execute(script, false);
            ScribeBot.Scripter.Environment.Globals["formdesign"] = typeof(formcontrol);
            object functiontable = ScribeBot.Scripter.Environment.Globals["testando"];
            ScribeBot.Scripter.Environment.Call(functiontable);
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
