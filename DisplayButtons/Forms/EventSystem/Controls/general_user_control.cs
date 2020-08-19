using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem.Controls
{
    public partial class general_user_control : UserControl
    {
        private static general_user_control instance;

        public static general_user_control Instance
        {
            get
            {
                return instance;
            }
        }
        public general_user_control()
        {
            instance = this;
            InitializeComponent();
        }
        public  string  GetName(){


            return textBox1.Text;
            }
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
