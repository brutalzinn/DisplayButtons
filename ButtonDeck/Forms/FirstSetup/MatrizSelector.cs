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
    public class MatrizSelector : UserControl
    {
        private TextBox textBox2;
        private Label label4;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;

        public virtual void SaveProgress()
        {

        }

        public virtual bool CanProgress { get; set; } = true;

        public ApplicationColorScheme CurrentTheme {
            get {
                return ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            }
        }

        public override Color ForeColor {
            get => base.ForeColor; set {
                base.ForeColor = value;
                FixForeColor(Controls.OfType<Control>());
            }
        }

        private void FixForeColor(IEnumerable<Control> enumerable)
        {
            enumerable.All(c => {
                FixForeColor(c.Controls.OfType<Control>());
                c.ForeColor = ForeColor;

                return true;
            });
        }

        public MatrizSelector()
        {
            MaximumSize = MinimumSize = Size = new Size(567, 244);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            Font = new Font(SystemFonts.MessageBoxFont.FontFamily, 12);
            if (DesignMode)
                ForeColor = Color.White;
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(211, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(172, 91);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(211, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(-17, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "{matriz.choiser}";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "{matriz.choiser}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "{matriz.choiser.linha}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "{matriz.choiser.colum}";
            // 
            // MatrizSelector
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "MatrizSelector";
            this.Size = new System.Drawing.Size(548, 163);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
