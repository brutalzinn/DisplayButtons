
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Forms.EventSystem.Controls;
using DisplayButtons.Forms.EventSystem.Controls.conditions.timers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem
{
    public partial class event_interface : TemplateForm
    {
        private static event_interface instance;

        public static event_interface Instance
        {
            get
            {
                return instance;
            }
        }
        public bool isUpdate { get; set; }

        public Event CurrentEvent { get; set; }
        public event_interface(Event _event = null)
        {
            instance = this;
            InitializeComponent();
            if (_event != null)
            { 
                CurrentEvent = _event; 
                isUpdate = true; 
                general_user_control.Instance.SetEnabled(_event.IsEnabled);
                general_user_control.Instance.SetName(_event.Name);
                foreach (AbstractAction item_action in _event.list_actions)
                {

                    action_user_control.Instance.Add(item_action);
                }
                foreach (AbstractTrigger item_trigger in _event.list_triggers)
                {

                    trigger_user_control.Instance.Add(item_trigger);
                }
                if (_event.conditions.timer_interval)
                {
                    conditions_user_control.Instance.set_timer(1);
       timer_interval.Instance.setTimerStart(_event.conditions.timer_interval_start);
                timer_interval.Instance.setTimerEnd(_event.conditions.timer_interval_end);
                }
                if (_event.conditions.timer_now)
                {
                    conditions_user_control.Instance.set_timer(2);
                timer_exact.Instance.setTimerExact(_event.conditions.timer_interval_now);

                }
                if (_event.conditions.timer_after)
                {
                    conditions_user_control.Instance.set_timer(4);
                    timer_exact.Instance.setTimerExact(_event.conditions.timer_interval_after);

                }
                if (_event.conditions.timer_before)
                {
                    conditions_user_control.Instance.set_timer(3);
                    timer_exact.Instance.setTimerExact(_event.conditions.timer_interval_before);

                }
                if (_event.conditions.timer_none)
                {
                    conditions_user_control.Instance.set_timer(0);

                }
                conditions_user_control.Instance.setLuaPath(_event.conditions.lua_path);
                


              
         
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trigger_user_control1_Load(object sender, EventArgs e)
        {

        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
       
  Event _event = new Event();
            Condition condition = new Condition();


            if (conditions_user_control.Instance.type_timer() != 0)
            {
                if (conditions_user_control.Instance.type_timer() == 1)
                {
                    condition.timer_interval_start = timer_interval.Instance.timer_start();
                    condition.timer_interval_end = timer_interval.Instance.timer_end();
                    condition.timer_interval = true;
                }
                if (conditions_user_control.Instance.type_timer() == 3)
                {
                    condition.timer_interval_before = timer_exact.Instance.TimerExact();
                    condition.timer_before = true;
                }
                if (conditions_user_control.Instance.type_timer() == 4)
                {
                    condition.timer_interval_after = timer_exact.Instance.TimerExact();
                    condition.timer_after = true;
                }
                if (conditions_user_control.Instance.type_timer() == 2)
                {
                    condition.timer_interval_now = timer_exact.Instance.TimerExact();
                    condition.timer_now = true;
                }
            }
            else
            {
                condition.timer_none = true;
              

            }
            condition.lua_path = conditions_user_control.Instance.getLuaPath();


            _event.list_actions = action_user_control.Instance.GetList();
            _event.list_triggers = trigger_user_control.Instance.GetList();
            _event.Name = general_user_control.Instance.GetName();
            _event.IsEnabled = general_user_control.Instance.GetEnabled();
            _event.conditions = condition;
            if (isUpdate)
            {


                var item = EventXml.Settings.Events.Where(i => i == CurrentEvent).FirstOrDefault();
                var index = EventXml.Settings.Events.IndexOf(item);

                if (index != -1)
                {
EventXml.Settings.Events[index] = _event;
                }
                    
                //  EventXml.Settings.Events.Where(x => x.Equals(CurrentEvent) == _event : x);




            }
            else
            {    
              
             EventXml.Settings.Events.Add(_event);
            }
         
         //   action_user_control.Instance = null;
         //   trigger_user_control.Instance = null;
         //   general_user_control.Instance = null;
        }
     
    }
   public static class GenericClass 
    {

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T oldValue, T newValue)
    => source.Select(x => x.Equals(oldValue) ? newValue : x);
    
    }
}
