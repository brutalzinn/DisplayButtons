using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Bibliotecas.DeckEvents;
using System.Linq;

namespace DisplayButtons.Forms.EventSystem.Controls.EventCreator.Controls
{
    public partial class action_control_new : UserControl
    {
        public action_control_new()
        {
            InitializeComponent();

            var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractAction>();

            foreach(var item in items)
            {

                comboBox1.Items.Add(item.GetActionName());
            }
        }
      
        private void action_control_new_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractAction>();
            var selected = items.Where(e => e.GetActionName() == comboBox1.Text).FirstOrDefault();
            panel1.Controls.Add(selected.OnSelect());
        }
    }
}
