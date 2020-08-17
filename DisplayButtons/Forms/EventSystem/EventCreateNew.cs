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
using System.Linq;
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

            var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractTrigger>();

            foreach (var item in items)
            {
               
                comboBox.Items.Add(item.GetActionName());
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractTrigger>();
            var selected = items.Where(e => e.GetActionName() == comboBox.Text).FirstOrDefault();
            panel1.Controls.Add(selected.OnSelect());
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
      



        private void EventCreateNew_Load(object sender, EventArgs e)
        {

        }
    }
}
