using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents;
using OpenQA.Selenium.Remote;

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
            FactoryForms.FactoryActionControl listview = new FactoryForms.FactoryActionControl();
            listview.Text = trigger.GetActionName();
            listview.Value = trigger;
            listBox1.Items.Add(listview);
        }
        private void imageModernButton3_Click(object sender, EventArgs e)
        {
            new FactoryForms().ToExecuteFormGeneral(1);





        }

        private void action_user_control_Load(object sender, EventArgs e)
        {

        }
        public void Remove()
        {

            listBox1.Items.Remove(listBox1.SelectedItem);

        }
        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            Remove();
        }
    }
}
