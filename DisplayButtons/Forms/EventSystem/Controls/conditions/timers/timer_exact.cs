using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem.Controls.conditions.timers
{
    public partial class timer_exact : UserControl
    {
        private static timer_exact instance;

        public static timer_exact Instance
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
        public timer_exact()
        {
            InitializeComponent();
        }
    }
}
