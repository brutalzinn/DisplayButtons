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
    public partial class WindowTrigger : PanelControl
    {

        private static WindowTrigger instance;

        public static WindowTrigger Instance
        {
            get
            {
                return instance;
            }
        }
        public WindowEvent window;
   

        public WindowTrigger(WindowEvent value)
        {
            instance = this;
            InitializeComponent();
          
            if (value != null)
            {
                window = value;

            textBox1.Text = value.AppName;
            }
          
        }
        public override void SaveConfig()
        {

            if (window != null)
            {
                window.AppName = textBox1.Text;
            }
     
        }

        public override UserControl getControl { get => this; }
        public override AbstractTrigger getClassImplementTrigger { get => window; }
        private void imageModernButton1_Click(object sender, EventArgs e)
        {
          //  WindowEvent windowevent = new WindowEvent();

           
        }

        private void WindowTrigger_Load(object sender, EventArgs e)
        {

        
        }

        private void imageModernButton2_Click(object sender, EventArgs e)
        {
       
        }
    }
}
