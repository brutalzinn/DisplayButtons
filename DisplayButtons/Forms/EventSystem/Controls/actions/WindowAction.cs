using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Misc;

namespace DisplayButtons.Forms.EventSystem.Controls.actions
{
    public partial class WindowAction : UserControl
    {
        public WindowAction()
        {
            InitializeComponent();
            loadFolders();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            ChangeFolder windowevent = new ChangeFolder();

 
           windowevent.folder = (DynamicDeckFolder)comboBox1.SelectedItem;
            new FactoryForms().SaveButton(windowevent);
        }
        public void loadFolders()
        {
            if (MainForm.Instance.CurrentDevice.GetConnection() != null)
            {
                var items = MainForm.ListFolders(MainForm.Instance.CurrentDevice.MainFolder as DynamicDeckFolder);

                foreach (DynamicDeckFolder present in items)
                {
                    FactoryForms.GlobalControl ComboBoxNew = new FactoryForms.GlobalControl();

                    ComboBoxNew.Text = present.DeckName;
                    ComboBoxNew.Value = present;
                    comboBox1.Items.Add(ComboBoxNew);
                }
            }

        }
        private void WindowAction_Load(object sender, EventArgs e)
        {
         
        }

        private void imageModernButton3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            loadFolders();
        }
    }
}
