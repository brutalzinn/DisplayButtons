using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace DisplayButtons.Bibliotecas.DeckEvents
{

    [Serializable]
    public class Event
    {

        public Event(){

            }

     

        public Event(List<AbstractAction> list_actions, List<AbstractTrigger> list_triggers, string name,bool isenabled)
        {
            this.list_actions = list_actions;
            this.list_triggers = list_triggers;
            Name = name;
            IsEnabled = isenabled;
        }
        [System.Xml.Serialization.XmlElementAttribute("EventActions", typeof(AbstractAction))]
        public List<AbstractAction> list_actions { get; set; } = new List<AbstractAction>();
        [System.Xml.Serialization.XmlElementAttribute("EventTriggers", typeof(AbstractTrigger))]
        public List<AbstractTrigger> list_triggers { get; set; } = new List<AbstractTrigger>();

        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("IsEnabled")]
        public bool IsEnabled { get; set; }

    }
}
