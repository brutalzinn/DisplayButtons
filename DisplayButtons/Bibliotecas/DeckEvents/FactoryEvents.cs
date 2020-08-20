using CookieProjects.ProcessWatcher;
using DisplayButtons.Backend.Utils;
using EventHook;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    class FactoryEvents
    {



        public void Init()
        {

            var reflexive_class = ReflectiveEnumerator.GetEnumerableOfType<AbstractTrigger>();
          
       
                
                foreach(var events in EventXml.Settings.Events)
                {
                 var totalFilterItems = events.list_triggers;


                var noduplicates = totalFilterItems.GroupBy(i => i.GetHashCode()).Select(i => i.FirstOrDefault()).ToList();

                // 
                foreach (var item in noduplicates)
                {
            //        item.OnInit();
Debug.WriteLine(item.GetActionName());
                }
                    

                
                   
                
                
                }

            TimedProcessWatcher teste = new TimedProcessWatcher(1.0);

            teste.Start();
            teste.ProcessStarted += (s, e) =>
            {

                Debug.WriteLine(string.Format("Key {0} event of key {1}", e.ProcessName,e.ProcessID));

            };




                
                //waiting here to keep this thread running           
                //Console.Read();

                //stop watching
                //keyboardWatcher.Stop();
                //mouseWatcher.Stop();
                //clipboardWatcher.Stop();
                //applicationWatcher.Stop();
                //printWatcher.Stop();
            


        }

    }
}
