using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.TwitchChat
{
    public partial class TwitchUserInput : TemplateForm
    {
        public TwitchUserInput()
        {
            InitializeComponent();
        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }
        private void TwitchUserInput_Load(object sender, EventArgs e)
        {

        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);

        }

        private void imageModernButton2_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);

        }
    }
}
