using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Backend;

namespace DisplayButtons.Forms.FirstSetup
{
    public partial class MatrizSelector : PageTemplate
    {
        public MatrizSelector()
        {
            InitializeComponent();
        }

        private void MatrizSelector_Load(object sender, EventArgs e)
        {
            Texts.initilizeLang();
            this.Refresh();
            label1.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_MATRIZ_LABEL1", Texts.cultereinfo);
            label2.Text =   Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_MATRIZ_LABEL2", Texts.cultereinfo);


            label3.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_MATRIZ_LABEL3", Texts.cultereinfo);

        }
        public override void SaveProgress()
        {
        
            if(coluna.Text.Length > 0 && linha.Text.Length > 0)
            {

           
           // ApplicationSettingsManager.Settings.coluna = Convert.ToInt32(coluna.Text);
        //    ApplicationSettingsManager.Settings.linha = Convert.ToInt32(linha.Text);

            }
        }
    }
}
