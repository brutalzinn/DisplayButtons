using ButtonDeck.Misc;
using ButtonDeck.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonDeck.Forms.FirstSetup
{
    public class MatrizSelector : PageTemplate
    {
        private Label label2;
        private Label label3;
        private TextBox linha;
        private TextBox coluna;
        private Label label1;

        public MatrizSelector()
        {
          
            label1.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_MATRIZ_LABEL1", Texts.cultereinfo);
            linha.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_MATRIZ_LABEL2", Texts.cultereinfo);
            coluna.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_MATRIZ_LABEL3", Texts.cultereinfo);

        }


        public override void SaveProgress()
        {
            ApplicationSettingsManager.Settings.coluna = Convert.ToInt32(coluna.Text);
            ApplicationSettingsManager.Settings.linha = Convert.ToInt32(linha.Text);
            ApplicationSettingsManager.SaveSettings();
        }
        private void MatrizSelector_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linha = new System.Windows.Forms.TextBox();
            this.coluna = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "matriz cool text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "linha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "coluna";
            // 
            // linha
            // 
            this.linha.Location = new System.Drawing.Point(98, 67);
            this.linha.Name = "linha";
            this.linha.Size = new System.Drawing.Size(448, 29);
            this.linha.TabIndex = 4;
            // 
            // coluna
            // 
            this.coluna.Location = new System.Drawing.Point(98, 131);
            this.coluna.Name = "coluna";
            this.coluna.Size = new System.Drawing.Size(448, 29);
            this.coluna.TabIndex = 5;
            // 
            // MatrizSelector
            // 
            this.Controls.Add(this.coluna);
            this.Controls.Add(this.linha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MatrizSelector";
            this.Load += new System.EventHandler(this.MatrizSelector_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MatrizSelector_Load_1(object sender, EventArgs e)
        {

        }
    }
}
