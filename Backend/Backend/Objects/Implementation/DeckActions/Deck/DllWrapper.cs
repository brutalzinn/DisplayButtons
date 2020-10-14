using McMaster.NETCore.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Objects.Implementation.DeckActions.Deck
{
    public class DllWrapper : AbstractDeckAction
    {
        [XmlIgnore]
        public PluginLoader myPlugin { get; set; }
      [ActionPropertyIncludeAttribute]
        public string config { get; set; } = "";
        [ActionPropertyIncludeAttribute]

        public string name { get; set; } = "";
      
        public void configHelper()
        {
            foreach (var pluginType in myPlugin
                           .LoadDefaultAssembly()
                           .GetTypes()
                           .Where(t => typeof(InterfaceDll.ButtonInterface).IsAssignableFrom(t) && !t.IsAbstract))
            {
                InterfaceDll.ButtonInterface meuplugin = (InterfaceDll.ButtonInterface)Activator.CreateInstance(pluginType);

                meuplugin.MenuHelper();

            }
        }
        public override AbstractDeckAction CloneAction()
        {
            return new DllWrapper();
        }

        public override DeckActionCategory GetActionCategory()
        {
            return DeckActionCategory.Deck;
        }

        public override string GetActionName()
        {
       
            return name;
        }
        public override bool IsPlugin()
        {
     
            return true;
        }

        public override void OnButtonDown(DeckDevice deckDevice)
        {

            foreach (var pluginType in myPlugin
                                .LoadDefaultAssembly()
                                .GetTypes()
                                .Where(t => typeof(InterfaceDll.ButtonInterface).IsAssignableFrom(t) && !t.IsAbstract))
            {
                InterfaceDll.ButtonInterface meuplugin = (InterfaceDll.ButtonInterface)Activator.CreateInstance(pluginType);

                meuplugin.OnButtonDown();
            }
        }

        public override void OnButtonUp(DeckDevice deckDevice)
        {

            foreach (var pluginType in myPlugin
                                .LoadDefaultAssembly()
                                .GetTypes()
                                .Where(t => typeof(InterfaceDll.ButtonInterface).IsAssignableFrom(t) && !t.IsAbstract))
            {
                InterfaceDll.ButtonInterface meuplugin = (InterfaceDll.ButtonInterface)Activator.CreateInstance(pluginType);

                meuplugin.OnButtonUp();
            }
        }
    }
}
