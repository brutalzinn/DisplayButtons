using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents;

namespace DisplayButtons.Forms.EventSystem.Controls
{
    public partial class trigger_user_control : UserControl
    {
        public trigger_user_control()
        {
            InitializeComponent();
        }
        public void Add(string item, AbstractTrigger trigger)
        {
            FactoryForms.ListView listview = new FactoryForms.ListView();
            listview.Text = item;
            listview.Value = trigger;
            listBox1.Items.Add(listview);
        }
        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            EventCreateNew teste = new EventCreateNew();
            teste.init(true);
            teste.Show();


        }

        private void trigger_user_control_Load(object sender, EventArgs e)
        {

        }
    }
}
