using ButtonDeck.Backend.Utils;
using ButtonDeck.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck.Forms.ActionHelperForms
{
    public partial class MainFormMenuOption : Form
    {
        public MainFormMenuOption()
        {
            InitializeComponent();
          
        }
    private void ApplySidebarTheme(Control parent)
        {
            //Headers have the theme's secondary color as background
            //and the theme's foreground color as text color
            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            parent.Controls.OfType<Control>().All((c) =>
            {
                if (c.Tag != null && c.Tag.ToString().ToLowerInvariant() == "header")
                {
                    c.BackColor = appTheme.SecondaryColor;
                    c.ForeColor = appTheme.ForegroundColor;
                }
                else
                {
                    c.BackColor = appTheme.BackgroundColor;
                }
                return true;
            });
        }
        private void MainFormMenuOption_Load(object sender, EventArgs e)
        {
            ApplySidebarTheme(shadedPanel1);
        }
    
        private string _toExecuteFileName;

       
        private static string scripter_form;
     
        public string scripter
        {
            get { return scripter_form; }
            set
            {
                scripter_form = value;


            }
        }
        public string ToExecuteFileName
        {
            get { return _toExecuteFileName; }
            set
            {
                _toExecuteFileName = value;
                //UpdateFinal(ModifiableAction);
            }
        }
        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }

        private void ModernButton2_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.OK);
        }

        private void ModernButton3_Click(object sender, EventArgs e)
        {
            CloseWithResult(DialogResult.Cancel);
        }
    }
}
