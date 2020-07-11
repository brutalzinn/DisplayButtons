using ButtonDeck.Backend.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Backend.Utils
{
    public class DeckActionHelper
    {
        public DeckActionHelper(AbstractDeckAction deckAction)
        {
            DeckAction = deckAction;
        }

        public AbstractDeckAction DeckAction { get; set; }
        public string ToExecute { get; set; }
        public string ToName{ get; set; }
    }
}
