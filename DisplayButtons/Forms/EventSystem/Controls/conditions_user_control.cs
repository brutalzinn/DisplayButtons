using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Forms.EventSystem.Controls.conditions.timers;

namespace DisplayButtons.Forms.EventSystem.Controls
{
    public partial class conditions_user_control : UserControl
    {
        public conditions_user_control()
        {
            InitializeComponent();
        }

        private void conditions_user_control_Load(object sender, EventArgs e)
        {

        }

        private void progressLog1_Load(object sender, EventArgs e)
        {

        }

        private void timer_interval_radio_CheckedChanged(object sender, EventArgs e)
        {
            if (timer_interval_radio.Checked)
            {
                panel1.Controls.Clear();
                panel1.Controls.Add(new timer_interval());

            }
            if (timer_exact_radio.Checked)
            {
                panel1.Controls.Clear();
                panel1.Controls.Add(new timer_exact());

            }
        }
    }
}
