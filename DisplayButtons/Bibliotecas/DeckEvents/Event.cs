using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
  public  class Event
    {
   

        public Event(List<AbstractAction> list_actions, List<AbstractTrigger> list_triggers, string name)
        {
            this.list_actions = list_actions;
            this.list_triggers = list_triggers;
            Name = name;
        }

        public List<AbstractAction> list_actions { get; set; } = new List<AbstractAction>();
        public List<AbstractTrigger> list_triggers { get; set; } = new List<AbstractTrigger>();

        public string Name { get; set; }


    }
}
