using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ButtonDeck.Backend.Utils;

namespace ButtonDeck.Forms.FirstSetup
{
    public partial class IntroPage : PageTemplate
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        private void IntroPage_Load(object sender, EventArgs e)
        {
            label1.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_INTROPAGE_LABEL1", Texts.cultereinfo);
            label2.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_INTROPAGE_LABEL2", Texts.cultereinfo);
            label4.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_INTROPAGE_LABEL3", Texts.cultereinfo);
            label5.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_INTROPAGE_LABEL4", Texts.cultereinfo);

            label6.Text = Texts.rm.GetString("LANGUAGE", Texts.cultereinfo);
        }

        public override void SaveProgress()
        {
            ApplicationSettingsManager.Settings.Language = comboBox1.Text;
          
            ApplicationSettingsManager.SaveSettings();
        }
    }
}
