using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Forms.EventSystem.Controls.conditions.timers;
using System.IO;

namespace DisplayButtons.Forms.EventSystem.Controls
{
    public partial class conditions_user_control : UserControl
    {

        private static conditions_user_control instance;

        public static conditions_user_control Instance
        {
            get
            {
                return instance;
            }
        }
        public conditions_user_control()
        {
            instance = this;
            InitializeComponent();
        }

        private void conditions_user_control_Load(object sender, EventArgs e)
        {

        }

        private void progressLog1_Load(object sender, EventArgs e)
        {

        }
        public string getLuaPath()

        {


            return textBox1.Text;
        
        }
        public int type_timer()
        {
            int type = 0;
            if (timer_interval_radio.Checked)
            {

                type = 1;
            }

            if (timer_exact_radio.Checked)
            {
                type = 2;
            }
            if (timer_none_radio_button.Checked)
            {
                type = 0;

            }
            return type;

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
            if (timer_none_radio_button.Checked)
            {
                panel1.Controls.Clear();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "LUA files|*.lua";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            textBox1.Text = theDialog.FileName;
                            // Insert code to read the stream here.
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
