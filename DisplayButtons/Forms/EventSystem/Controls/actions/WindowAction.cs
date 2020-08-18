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
using DisplayButtons.Backend.Objects;

namespace DisplayButtons.Forms.EventSystem.Controls.actions
{
    public partial class WindowAction : PanelControl
    {
        public ChangeFolder window;
        public WindowAction(ChangeFolder value)
        {
            InitializeComponent();

            if (value != null)
            {
                window = value;

              comboBox1.SelectedItem = value.folder;
            }

            loadFolders();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
            ChangeFolder windowevent = new ChangeFolder();

 
           windowevent.folder = comboBox1.SelectedItem as DynamicDeckFolder;
            new FactoryForms().SaveButtonAction(windowevent);
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
        public override void SaveConfig()
        {

            if (window != null)
            {
           //     window.AppName = textBox1.Text;
            }

        }

        public override UserControl getControl { get => this; }
        public override AbstractAction getClassImplementAction { get => window; }
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
