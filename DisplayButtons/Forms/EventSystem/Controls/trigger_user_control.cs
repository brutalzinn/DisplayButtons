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
        private static trigger_user_control instance;

        public static trigger_user_control Instance
        {
            get
            {
                return instance;
            }
        }
        public trigger_user_control()
        {
            instance = this;
            InitializeComponent();
        }
        public void Add(AbstractTrigger trigger)
        {
            FactoryForms.GlobalControl listview = new FactoryForms.GlobalControl();
            listview.Text = trigger.GetActionName();
            listview.Value = trigger;
            listBox1.Items.Add(listview);
        }

        public void Remove(AbstractTrigger trigger)
        {

            listBox1.Items.Remove(listBox1.SelectedItem);

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
