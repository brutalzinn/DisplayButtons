using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem.Controls
{
    public partial class action_user_control : UserControl
    {
        public action_user_control()
        {
            InitializeComponent();
        }

        private void imageModernButton3_Click(object sender, EventArgs e)
        {
            EventCreateNew teste = new EventCreateNew();
            teste.init(false);
            teste.Show();
        }
    }
}
