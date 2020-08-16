using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
  public  class Event
    {
        public Event(string name, AbstractAction abstractAction)
        {
            Name = name;
            AbstractAction = abstractAction;
        }

        public string Name { get; set; }
        public AbstractAction AbstractAction { get; set; }

    }
}
