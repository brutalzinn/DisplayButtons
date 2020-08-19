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
          
          foreach(var item in EventXml.Settings.Events)
            {
  FactoryForms.FactoryEventControl listview = new FactoryForms.FactoryEventControl();
                listview.Text = item.Name;//trigger.GetActionName();
            listview.Value = item;// trigger;
            listBox1.Items.Add(listview);

            }
        
        }
        public FactoryForms.FactoryEventControl CurrentItem { get; set; }
        private void Events_Load(object sender, EventArgs e)
        {

        }

        private void imageModernButton3_Click(object sender, EventArgs e)
        {
            event_interface teste = new event_interface();
            teste.Show();
               
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listBox1.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                CurrentItem = (FactoryForms.FactoryEventControl)listBox1.Items[intselectedindex];

                //do something
                //MessageBox.Show(listView1.Items[intselectedindex].Text); 
            }

        }

        private void imageModernButton4_Click(object sender, EventArgs e)
        {
            event_interface teste = new event_interface(CurrentItem.Value);
            teste.Show();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(CurrentItem.Value);
            EventXml.Settings.Events.Remove(CurrentItem.Value);
        }
    }
}
