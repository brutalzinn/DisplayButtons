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

          textBox1.Text = value.AppName;
            }
          
        }
        public override void SaveConfig()
        {

            if (window != null)
            {
              window.AppName = textBox1.Text;  
          
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
    }
}
