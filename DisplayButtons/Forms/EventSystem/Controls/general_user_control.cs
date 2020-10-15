using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BackendAPI;

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
            set
            {
                instance = value;
            }
        }
        public general_user_control()
        {
            instance = this;
            InitializeComponent();

            label1.Text = Texts.rm.GetString("EVENTSYSTEMEVENTNAME", Texts.cultereinfo); 
            checkBox1.Text = Texts.rm.GetString("EVENTSYSTEMISACTIVE", Texts.cultereinfo); 
        }
        public  string  GetName(){


            return textBox1.Text;
            }
        public bool GetEnabled()
        {


            return checkBox1.Checked;
        }
        public void SetName(string name)
        {


          textBox1.Text = name;
        }
        public void SetEnabled(bool value)
        {


             checkBox1.Checked = value;
        }
        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
