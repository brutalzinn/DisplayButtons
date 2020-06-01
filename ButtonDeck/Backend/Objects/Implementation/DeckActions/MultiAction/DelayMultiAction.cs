using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects.Implementation.DeckActions.MultiAction
{



    public class DelayMultiAction : AbstractDeckAction
    {




        public override string GetActionName() => "Delay";
        public override AbstractDeckAction CloneAction()
        {
            return new DelayMultiAction();
        }
        public override bool IsTool()
        {
            return true;
        }
        public override DeckActionCategory GetActionCategory() => DeckActionCategory.General;
        public override void OnButtonDown(DeckDevice deckDevice)
        {



          
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {
        }

    }
}
