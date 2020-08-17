using DisplayButtons.Forms.EventSystem.Controls.EventCreator.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public bool _type;
        // if true, create a trigger 
        // if false, create a action
        public void init(bool type)
        {
            if (type)
            {
               
                panel1.Controls.Clear();
                panel1.Controls.Add(new trigger_control_new());
            }
            else
            {
                panel1.Controls.Clear();
                panel1.Controls.Add(new action_control_new());
            }
            _type = type;
        }


        public void ShouldUpdate(object value)
        {
            if(_type)
            {
             //    trigger_control_new

            }
           

        }


        private void EventCreateNew_Load(object sender, EventArgs e)
        {

        }
    }
}
