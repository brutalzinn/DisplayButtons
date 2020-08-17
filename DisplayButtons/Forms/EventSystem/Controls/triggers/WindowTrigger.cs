using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;

namespace DisplayButtons.Forms.EventSystem.Controls.triggers
{
    public partial class WindowTrigger : UserControl
    {
        public WindowTrigger()
        {
            InitializeComponent();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            new FactoryForms().SaveButton( new WindowEvent(), textBox1.Text);
        }
    }
}
