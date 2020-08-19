using DisplayButtons.Bibliotecas.DeckEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem
{
    public partial class Events : TemplateForm
    {
        public Events()
        {
            InitializeComponent();
            FactoryForms.FactoryEventControl listview = new FactoryForms.FactoryEventControl();
          foreach(var item in EventXml.Settings.Events)
            {

                listview.Text = item.Name;//trigger.GetActionName();
            listview.Value = item;// trigger;
            listBox1.Items.Add(listview);

            }
        
        }

        private void Events_Load(object sender, EventArgs e)
        {

        }

        private void imageModernButton3_Click(object sender, EventArgs e)
        {
            event_interface teste = new event_interface();
            teste.Show();
               
        }
    }
}
