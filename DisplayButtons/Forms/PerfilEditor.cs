using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms
{
    public partial class PerfilEditor : TemplateForm
    {
        public PerfilEditor()
        {
            InitializeComponent();
        }

        private void PerfilEditor_Load(object sender, EventArgs e)
        {

        }
        
        private void OK_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);
        }

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }
    }
}
