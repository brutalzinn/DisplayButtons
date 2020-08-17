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
    public partial class action_user_control : UserControl
    {
        private static action_user_control instance;

        public static action_user_control Instance
        {
            get
            {
                return instance;
            }
        }

        public action_user_control()
        {
            instance = this;
            InitializeComponent();
        }
        public void Add(AbstractAction trigger)
        {
            FactoryForms.GlobalControl listview = new FactoryForms.GlobalControl();
            listview.Text = trigger.GetActionName();
            listview.Value = trigger;
            listBox1.Items.Add(listview);
        }
        private void imageModernButton3_Click(object sender, EventArgs e)
        {
            EventCreateNew teste = new EventCreateNew();
            teste.init(false);
            teste.Show();
        }

        private void action_user_control_Load(object sender, EventArgs e)
        {

        }
    }
}
