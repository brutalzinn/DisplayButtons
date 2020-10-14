using CookieProjects.ProcessWatcher;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using MoreLinq.Extensions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
  public  class FactoryEvents
    {

        public static TimedProcessWatcher processWatcher;
        static readonly FactoryEvents _instance = new FactoryEvents();
        public static FactoryEvents Instance
        {
            get
            {
                return _instance;
            }
        }
        FactoryEvents()
        {
            // Initialize.
        }
        public static void Init()
        {
         
  
       
            //start commum events
            foreach (Event events in EventXml.Settings.Events)
            {
                if (events.IsEnabled)
                {
                    foreach (var item in events.list_triggers)
                    {

                        item.OnInit(events);


                    }
                }
            }


          

            }
    
       
    }
}
