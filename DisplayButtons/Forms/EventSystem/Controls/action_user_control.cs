using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents;
using OpenQA.Selenium.Remote;
using System.Reflection;
using System.Diagnostics;
using Backend;

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
            set
            {
                instance = value;
            }
        }
        public FactoryForms.FactoryActionControl CurrentItem { get; set; }
        public action_user_control()
        {
            instance = this;
            InitializeComponent();



         imageModernButton1.Text =   Texts.rm.GetString("EVENTSYSTEMDELETEBUTTON", Texts.cultereinfo);
        imageModernButton2.Text    = Texts.rm.GetString("EVENTSYSTEMCONFIGBUTTON", Texts.cultereinfo);
        imageModernButton3.Text =     Texts.rm.GetString("EVENTSYSTEMNEWBUTTON", Texts.cultereinfo);

        }   
        public List<AbstractAction> list_actions { get; set; } = new List<AbstractAction>();
        public List<AbstractAction> GetList()
        {

        

           foreach(FactoryForms.FactoryActionControl item in listBox1.Items)
            {

                list_actions.Add(item.Value);

            }
            return list_actions;
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

        private void imageModernButton2_Click(object sender, EventArgs e)
        {
            if (CurrentItem != null)
            {
                MethodInfo helperMethod = CurrentItem.Value.GetType().GetMethod("ToExecuteHelper");
                if (helperMethod != null)
                {

                    helperMethod.Invoke(CurrentItem.Value, null);

                }

            }
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
                CurrentItem = (FactoryForms.FactoryActionControl)listBox1.Items[intselectedindex];

                //do something
                //MessageBox.Show(listView1.Items[intselectedindex].Text); 
            }

        }

        private void action_user_control_Leave(object sender, EventArgs e)
        {


        //    Debug.WriteLine("FECHANDO ACTION INSTANCE");
        
        }
    }
}
