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
using DisplayButtons.Forms.EventSystem.misc.ProcessTrigger;
using DisplayButtons.BackendAPI.Objects;
using BackendAPI.Objects;
using BackendAPI;

namespace DisplayButtons.Forms.EventSystem.Controls.triggers
{
    public partial class WindowTrigger : PanelControl
    {

        
        public WindowEvent window;
   

        public WindowTrigger(WindowEvent value)
        {
            
            InitializeComponent();

            label1.Text = Texts.rm.GetString("WINDOWTRIGGERLABEL1", Texts.cultereinfo);
            label2.Text = Texts.rm.GetString("WINDOWTRIGGERLABEL2", Texts.cultereinfo);
            button1.Text = Texts.rm.GetString("WINDOWTRIGGERBUTTON", Texts.cultereinfo);
            Dictionary<int, string> test = new Dictionary<int, string>();
            test.Add(1, Texts.rm.GetString("WINDOWTRIGGERWINDOWTYPEOPEN", Texts.cultereinfo));
            test.Add(0, Texts.rm.GetString("WINDOWTRIGGERWINDOWTYPCLOSE", Texts.cultereinfo));
            comboBox1.DataSource = new BindingSource(test, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            if (value != null)
            {
                window = value;

            textBox1.Text = value.AppName;
                // int value = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
                comboBox1.SelectedValue = value.windowEvent;
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

        private void button1_Click(object sender, EventArgs e)
        {
            dynamic form = Activator.CreateInstance(AbstractDeckAction.FindType("DisplayButtons.Forms.EventSystem.misc.ProcessTrigger.ProcessList")) as Form;
            if (form.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = form.listBox1.SelectedItem.ToString();
            }
            else
            {
                form.Close();
            }

        }

        }
}
