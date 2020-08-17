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

        private static WindowTrigger instance;

        public static WindowTrigger Instance
        {
            get
            {
                return instance;
            }
        }
        public WindowTrigger()
        {
            instance = this;
            InitializeComponent();
        }
        public void loadInfo(AbstractTrigger value)
        {
            if(value is WindowEvent FF)
            {
                textBox1.Text = FF.AppName;

            }


        }
        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            WindowEvent windowevent = new WindowEvent();

                windowevent.AppName = textBox1.Text;
            new FactoryForms().SaveButton(windowevent);
        }
    }
}
