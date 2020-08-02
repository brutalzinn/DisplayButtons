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

        }

        public override void SaveProgress()
        {
            ApplicationSettingsManager.Settings.Language = comboBox1.Text;
          
            ApplicationSettingsManager.SaveSettings();
        }
    }
}
