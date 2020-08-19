
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Forms.EventSystem.Controls;
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
        public event_interface(Event _event = null)
        {
            instance = this;
            InitializeComponent();
            if (_event != null)
            {
                foreach (AbstractAction item_action in _event.list_actions)
                {

                    action_user_control.Instance.Add(item_action);
                }
                foreach (AbstractTrigger item_trigger in _event.list_triggers)
                {

                    trigger_user_control.Instance.Add(item_trigger);
                }
                general_user_control.Instance.SetEnabled(_event.IsEnabled);
                general_user_control.Instance.SetName(_event.Name);
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
            _event.list_actions = action_user_control.Instance.GetList();
            _event.list_triggers = trigger_user_control.Instance.GetList();
            _event.Name = general_user_control.Instance.GetName();
            _event.IsEnabled = general_user_control.Instance.GetEnabled();
          
            
          
            if (EventXml.Settings.Events.Contains(_event))
            {
              var old_event =  EventXml.Settings.Events.Where(i => i == _event).FirstOrDefault();
                var index = EventXml.Settings.Events.IndexOf(old_event);

                if (index != -1)
                    EventXml.Settings.Events[index] = _event;

            }
            else
            {
   EventXml.Settings.Events.Add(_event);
            }
         
            
        }
    }
}
