using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem.Controls.conditions.timers
{
    public partial class timer_interval : UserControl
    {
        private static timer_interval instance;

        public static timer_interval Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        public timer_interval()
        {
            InitializeComponent();
        }

        private void timer_interval_Load(object sender, EventArgs e)
        {

        }
    }
}
