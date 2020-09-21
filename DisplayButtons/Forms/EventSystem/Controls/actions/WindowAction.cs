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
            label1.Text = Texts.rm.GetString("FOLDERPLACEHOLDER", Texts.cultereinfo);
            if (value != null)
            {
                window = value;
               if(value.folder != null)
                {

 comboBox1.SelectedItem = value.folder;
                }
             
            }

            loadFolders();
        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
           
        }
        public void loadFolders()
        {
            if (MainForm.Instance.CurrentDevice.GetConnection() != null)
            {
                var items = MainForm.ListFolders(MainForm.Instance.CurrentDevice.CurrentProfile.Mainfolder as DynamicDeckFolder);

                foreach (DynamicDeckFolder present in items)
                {
                    FactoryForms.GlobalControl ComboBoxNew = new FactoryForms.GlobalControl();

                    ComboBoxNew.Text = present.GetDeckDefaultLayer.Deckname;
                    ComboBoxNew.Value = present;
                    comboBox1.Items.Add(ComboBoxNew);
                }
            }

        }
   
        public override void SaveConfig()
        {

            if (window != null)
            {


                window.folder = (DynamicDeckFolder)comboBox1.SelectedItem;
           
             
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
