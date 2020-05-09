using ButtonDeck.Backend.Utils;
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
      
   [XmlIgnore]
        public static string UniqueIdentifier { get; private set; }

        public override bool IsPlugin()
        {
            return true;
        }
        public FolderAddAction()
        {
            UniqueIdentifier = new Guid().ToString();
           // name = input;
        }
       

        [ActionPropertyInclude]
        [ActionPropertyDescription("name")]
          public static string name { get; set; } = "teste";

        public static String[] LuaExecuteButtonDown { get; set; } = null;
        public static String[] LuaExecuteButtonUP { get; set; } = null;
        public static string DeckActionCategory_string { get; set; } = "Deck";
        public void ToExecuteHelper()
        {
        
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
            ScribeBot.Scripter.InjectLine(@LuaExecuteButtonDown.ToString());



        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
            ScribeBot.Scripter.InjectLine(@LuaExecuteButtonUP.ToString());

        }
    }
}
