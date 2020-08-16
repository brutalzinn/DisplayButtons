using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
  public  class Event
    {
        public Event(string name,List<AbstractAction> list_actions)
        {
            this.list_actions = list_actions;
            Name = name;
         
        }

        public List<AbstractAction> list_actions { get; set; } = new List<AbstractAction>();


        public string Name { get; set; }


    }
}
