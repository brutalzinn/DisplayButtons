using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using static DisplayButtons.Bibliotecas.DeckEvents.FactoryForms;

namespace DisplayButtons.Forms.EventSystem.Controls.triggers
{
    public partial class WindowTrigger : PanelControl
    {

        
        public WindowEvent window;
   

        public WindowTrigger(WindowEvent value)
        {
            
            InitializeComponent();
            Dictionary<int, string> test = new Dictionary<int, string>();
            test.Add(0, "Fechar");
            test.Add(1, "Abrir");
            comboBox1.DataSource = new BindingSource(test, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            if (value != null)
            {
                window = value;

            textBox1.Text = value.AppName;
                comboBox1.SelectedItem = value.windowEvent;
            }
          
        }
        public override void SaveConfig()
        {

            if (window != null)
            {
                int value = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;

                window.AppName = textBox1.Text;
                window.windowEvent = value;
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
