using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Bibliotecas.DeckEvents.Actions;

namespace DisplayButtons.Forms.EventSystem.Controls.triggers
{
    public partial class TimerTrigger : PanelControl
    {

        
        public TimerEvent window;
   

        public TimerTrigger(TimerEvent value)
        {
            
            InitializeComponent();
          
            if (value != null)
            {
                window = value;
            //    dateTimePicker1.CustomFormat = "dd-MM-yyyy hh:mm:ss";
                if (value.recurring)
                {
                    
                    recurring_timer_radio.Checked = true;
                    Datetime_radio.Checked = false;
                }
                else
                {
                    recurring_timer_radio.Checked = false;
                    Datetime_radio.Checked = true;
                    dateTimePicker1.Visible = true;
                    if(value.Start != null)
                    {

    dateTimePicker1.Value = value.Start;
                    }
                
                }

            }
          
        }
      
        public override void SaveConfig()
        {

            if (window != null)
            {
                // window.AppName = textBox1.Text;  

                

                if (recurring_timer_radio.Checked)
                {
                    window.recurring = true;
                 window.Interval = TimeSpan.Parse(textBox1.Text);
                }
              if(Datetime_radio.Checked)
                {
                    
                    window.recurring = false;
                    Datetime_radio.Checked = true;
                 window.Start = dateTimePicker1.Value; 
                   // dateTimePicker1.Value 
                }

            }

          

        }
  
       public override UserControl getControl { get => this; }
        public override AbstractTrigger getClassImplementTrigger { get => window; }
        private void imageModernButton1_Click(object sender, EventArgs e)
        {
          //  TimerEvent TimerEvent = new TimerEvent();

           
        }

        private void TimerTrigger_Load(object sender, EventArgs e)
        {

        
        }

        private void imageModernButton2_Click(object sender, EventArgs e)
        {
       
        }

        private void TimerTrigger_Load_1(object sender, EventArgs e)
        {

        }

        private void recurring_timer_radio_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
