using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem.misc.ProcessTrigger
{
    public partial class ProcessList : Form
    {
        public ProcessList()
        {
            InitializeComponent();
            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {
               listBox1.Items.Add(theprocess.ProcessName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void FindMyString(string searchString)
        {
            // Ensure we have a proper string to search for.
            if (!string.IsNullOrEmpty(searchString))
            {
                // Find the item in the list and store the index to the item.
                int index = listBox1.FindString(searchString);
                // Determine if a valid index is returned. Select the item if it is valid.
                if (index != -1)
                    listBox1.SetSelected(index, true);
                else
                    MessageBox.Show("The search string did not match any items in the ListBox");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            FindMyString(textBox1.Text);
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);
        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void imageModernButton2_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }
    }
}
