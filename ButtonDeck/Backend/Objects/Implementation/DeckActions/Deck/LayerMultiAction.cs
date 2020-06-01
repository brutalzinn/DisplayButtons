using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck.Backend.Objects.Implementation.DeckActions.Deck
{
    public class LayerMultiAction : AbstractDeckAction
    {
        [ActionPropertyInclude]
        [ActionPropertyDescription("name")]
        public string name { get; set; } = "Multi Action Button";


        [ActionPropertyInclude]
        [ActionPropertyDescription("To Execute")]
        public string ToExecute { get; set; } = "";
        public string script { get; set; } = "";



        [ActionPropertyInclude]
        public ArrayList list_multiple_actions { get; set; } = new ArrayList();
        public List<AbstractDeckAction> list_actions { get; set; } = new List<AbstractDeckAction>();

        public void ToExecuteHelper()
        {
           // var originalToExec = new String(ToExecute.ToCharArray());
            dynamic form = Activator.CreateInstance(FindType("ButtonDeck.Forms.ActionHelperForms.LayerMultiActionHelper")) as Form;
      
            form.list_actions = list_actions;
            if (form.ShowDialog() == DialogResult.OK)
            {
                //   ToExecute = form.ModifiableAction.ToExecute;
                list_actions = form.list_actions;
            }
            else
            {

             //   form.list_actions = list_actions;
            }
        }

        public override AbstractDeckAction CloneAction()
        {
            return new LayerMultiAction();
        }

        public override DeckActionCategory GetActionCategory() => DeckActionCategory.General;

        public override string GetActionName() => name;



        public override void OnButtonDown(DeckDevice deckDevice)
        {

          

        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
        }
    }
}
