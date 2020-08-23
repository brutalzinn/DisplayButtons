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
           
            //start especial events

            foreach (var events in EventXml.Settings.Events)
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
       
            //start commum events
            foreach (var events in EventXml.Settings.Events)
            {

         foreach(var item in events.list_triggers)
                {
                    if (item is TimerEvent)
                    {
                        item.OnInit(events);
                        SharpShedule.Sheduler teste = new SharpShedule.Sheduler();
                        teste.Shedule(Display, DateTime.Now.AddSeconds(5), events);
                        teste.Start();
                            
                    }
             //       item.OnInit();
                }
            }


                //waiting here to keep this thread running           
                //Console.Read();

                //stop watching
                //keyboardWatcher.Stop();
                //mouseWatcher.Stop();
                //clipboardWatcher.Stop();
                //applicationWatcher.Stop();
                //printWatcher.Stop();



            }
        void Display(Event value)
        {
            Debug.WriteLine("TESTINNGg.." + value.Name);
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

                                helperMethod.Invoke(CurrentItem, new object[] { e.Process, 1, events }) ;

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

                                helperMethod.Invoke(CurrentItem, new object[] { e.Process, 0, events });

                            }
                        }
                    }
                }
            };


        }
    }
}
