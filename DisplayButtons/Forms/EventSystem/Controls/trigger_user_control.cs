using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem.Controls
{
    public partial class trigger_user_control : UserControl
    {
        public trigger_user_control()
        {
            InitializeComponent();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            EventCreateNew teste = new EventCreateNew();
            teste.init(true);
            teste.Show();


        }
    }
}
