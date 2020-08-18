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
        public string AppNameTeste;

        public WindowTrigger(WindowEvent value = null)
        {
            instance = this;
            InitializeComponent();
            if(value != null)
            {
  AppNameTeste = value.AppName;
            textBox1.Text = AppNameTeste;
            }
          
        }


        private void CloseWithResult(DialogResult result)
        {
      
        }

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
