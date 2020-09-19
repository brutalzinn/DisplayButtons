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

            label1.Text = Texts.rm.GetString("EVENTSYSTEMEVENTNAME", Texts.cultereinfo);
            OK.Text = Texts.rm.GetString("BUTTONSAVE", Texts.cultereinfo);
            Cancel.Text = Texts.rm.GetString("BUTTONCANCEL", Texts.cultereinfo);
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
