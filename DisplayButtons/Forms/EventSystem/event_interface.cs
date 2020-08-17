
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public event_interface()
        {
            instance = this;
            InitializeComponent();
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
    }
}
