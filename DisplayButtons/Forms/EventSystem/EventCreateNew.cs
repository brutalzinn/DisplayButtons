using DisplayButtons.Backend.Utils;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;
using DisplayButtons.Forms.EventSystem.Controls.actions;

using DisplayButtons.Forms.EventSystem.Controls.triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Forms.EventSystem
{
    public partial class EventCreateNew : TemplateForm
    {
       
        public EventCreateNew()
        {
            InitializeComponent();
         imageModernButton1.Text = Texts.rm.GetString("EVENTSYSTEMSAVEBUTTON", Texts.cultereinfo);
            imageModernButton1.Text = Texts.rm.GetString("EVENTSYSTEMCANCELBUTTON", Texts.cultereinfo);
            this.Text = Texts.rm.GetString("EVENTSYSTEMBUTTON", Texts.cultereinfo);
        }
        public void FillComboBox(int type)
        {

            
            if (type == 0){


                abstracktrigger_list = ReflectiveEnumerator.GetEnumerableOfType<AbstractTrigger>();
                foreach (var item in abstracktrigger_list)
                {

                    comboBox.Items.Add(item.GetActionName());
                }
            }
            if(type == 1)
            {
                abstrackaction_list =  ReflectiveEnumerator.GetEnumerableOfType<AbstractAction>();
                foreach (var item in abstrackaction_list)
                {

                    comboBox.Items.Add(item.GetActionName());
                }
            }
   _type = type;

        }
        public int _type { get; set; } 
        public IEnumerable<AbstractTrigger> abstracktrigger_list;

        public IEnumerable<AbstractAction> abstrackaction_list;
        public PanelControl global_panelControl { get;set; }
        public void UpdateForm(PanelControl value,int type)
        {
         
            FillComboBox(type);
           
            panel1.Controls.Clear();
            //    global_panelControl = value.OnSelect();
            global_panelControl = value;
            panel1.Controls.Add(value);
          ///  TextBox control = global_panelControl.getControl.Controls.Find("textBox1", true).FirstOrDefault() as TextBox;
        
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           

 panel1.Controls.Clear();
            if (_type == 0 )
            { 
                AbstractTrigger selected = abstracktrigger_list.Where(e => e.GetActionName() == comboBox.Text).FirstOrDefault();
                global_panelControl = selected.OnSelect();
          

            }
             if (_type == 1)
            {

                 AbstractAction selected = abstrackaction_list.Where(e => e.GetActionName() == comboBox.Text).FirstOrDefault();
                global_panelControl = selected.OnSelect();
            }

           
           
            panel1.Controls.Add(global_panelControl);
           
        //    panel1.Controls.Add(global_trigger.getControl);

        }
        private AbstractAction _modifiableAction;
  
        public AbstractAction ModifiableAction
        {
            get { return _modifiableAction; }
            set
            {
                _modifiableAction = value;


            }
        }
        private AbstractTrigger _modifiableTrigger;

        public AbstractTrigger ModifableTrigger
        {
            get { return _modifiableTrigger; }
            set
            {
                _modifiableTrigger = value;


            }
        }

     
        // if true, create a trigger 
        // if false, create a action

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }



        private void EventCreateNew_Load(object sender, EventArgs e)
        {

        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
          
            CloseWithResult(DialogResult.OK);
        }

        private void imageModernButton2_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }
    }
}
