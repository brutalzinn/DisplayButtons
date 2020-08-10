using DisplayButtons.Backend.Utils;
using DisplayButtons.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayButtons.Forms.ActionHelperForms
{
    public partial class MainFormMenuOption : TemplateForm
    {
        public MainFormMenuOption()
        {
            InitializeComponent();
          
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            

        }
           
        private void MainFormMenuOption_Load(object sender, EventArgs e)
        {
            Texts.initilizeLang();
            this.Text = Texts.rm.GetString("INITIALIZER_MODE_FORMMENUOPTIONS_TEXT", Texts.cultereinfo);
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

        private void ShadedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
