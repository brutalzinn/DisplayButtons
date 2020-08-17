using DisplayButtons.Backend.Utils;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using DisplayButtons.Forms.EventSystem.Controls.actions;

using DisplayButtons.Forms.EventSystem.Controls.triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem
{
    public partial class EventCreateNew : TemplateForm
    {
        public EventCreateNew()
        {
            InitializeComponent();
        }
        private AbstractAction _modifiableAction;
  
        public AbstractAction ModifiableAction
        {
            get { return _modifiableAction; }
            set
            {
                _modifiableAction = value;


            }
        }
        private AbstractTrigger _modifiableTrigger;

        public AbstractTrigger ModifableTrigger
        {
            get { return _modifiableTrigger; }
            set
            {
                _modifiableTrigger = value;


            }
        }
        public bool _type;
        // if true, create a trigger 
        // if false, create a action
        public void init(bool type)
        {
            if (type)
            {
               
                panel1.Controls.Clear();
           //     panel1.Controls.Add(new trigger_control_new());
            }
            else
            {
                panel1.Controls.Clear();
             //   panel1.Controls.Add(new action_control_new());
            }
            _type = type;
        }

        public void ToUpdate(AbstractTrigger value)
        {
       //     var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractTrigger>();
       
         

           


        }


        private void EventCreateNew_Load(object sender, EventArgs e)
        {

        }
    }
}
