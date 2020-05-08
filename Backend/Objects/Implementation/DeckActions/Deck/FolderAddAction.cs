using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NickAc.Backend.Objects.Implementation.DeckActions.General
{
    public class FolderAddAction : AbstractDeckAction
    {

        [ActionPropertyInclude]
        [ActionPropertyDescription("name")]
        public static string name { get; set; } = "teste";

        public  void update(string result)
        {
            Debug.WriteLine("ATUALIZANDO.. " + result);
            name = result;

     
        }

        public void ToExecuteHelper()
        {
        
        }

        public override AbstractDeckAction CloneAction()
        {
           return new FolderAddAction();
        }

        public override DeckActionCategory GetActionCategory() => DeckActionCategory.Deck;

        public override string GetActionName()
        {
     
            return name;
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {
           



        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {

           
        }
    }
}
