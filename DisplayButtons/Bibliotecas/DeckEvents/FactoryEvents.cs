using CookieProjects.ProcessWatcher;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using EventHook;
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
if(item is WindowEvent)
                    {
                        EventProcess();
                        goto final;
                    }
                }
    
                }
            final:;
          


                
                //waiting here to keep this thread running           
                //Console.Read();

                //stop watching
                //keyboardWatcher.Stop();
                //mouseWatcher.Stop();
                //clipboardWatcher.Stop();
                //applicationWatcher.Stop();
                //printWatcher.Stop();
            


        }
        public void EventProcess()
        {

            TimedProcessWatcher teste = new TimedProcessWatcher(1.0);

            teste.Start();
            teste.ProcessStarted += (s, e) =>
            {
                foreach (var events in EventXml.Settings.Events)
                {
                    foreach (var CurrentItem in events.list_triggers)
                    {
                        if (CurrentItem is WindowEvent)
                        {
                            var totalFilterItems = events.list_triggers;
                            MethodInfo helperMethod = CurrentItem.GetType().GetMethod("ProcessHelper");
                            if (helperMethod != null)
                            {

                                helperMethod.Invoke(CurrentItem, new object[] { e.Process, 1 }) ;

                            }
                        }
                    }
                }
            };
            teste.ProcessStopped += (s, e) =>
            {
                foreach (var events in EventXml.Settings.Events)
                {
                    foreach (var CurrentItem in events.list_triggers)
                    {
                        if (CurrentItem is WindowEvent)
                        {
                            var totalFilterItems = events.list_triggers;
                            MethodInfo helperMethod = CurrentItem.GetType().GetMethod("ProcessHelper");
                            if (helperMethod != null)
                            {

                                helperMethod.Invoke(CurrentItem, new object[] { e.Process, 0 });

                            }
                        }
                    }
                }
            };


        }
    }
}
