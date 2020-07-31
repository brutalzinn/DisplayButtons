namespace ButtonDeck.Forms.ActionHelperForms
{
    partial class MainFormMenuOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.shadedPanel1 = new ButtonDeck.Forms.ShadedPanel();
            this.modernButton4 = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.modernButton2 = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.shadedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shadedPanel1
            // 
            this.shadedPanel1.ColorScheme = null;
            this.shadedPanel1.Controls.Add(this.modernButton4);
            this.shadedPanel1.Controls.Add(this.modernButton2);
            this.shadedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadedPanel1.Location = new System.Drawing.Point(0, 0);
            this.shadedPanel1.Name = "shadedPanel1";
            this.shadedPanel1.Size = new System.Drawing.Size(362, 189);
            this.shadedPanel1.TabIndex = 12;
            // 
            // modernButton4
            // 
            this.modernButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modernButton4.CustomColorScheme = false;
            this.modernButton4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.modernButton4.Location = new System.Drawing.Point(95, 107);
            this.modernButton4.Name = "modernButton4";
            this.modernButton4.Size = new System.Drawing.Size(142, 39);
            this.modernButton4.TabIndex = 10;
            this.modernButton4.Text = "USB";
            this.modernButton4.UseVisualStyleBackColor = true;
            // 
            // modernButton2
            // 
            this.modernButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modernButton2.CustomColorScheme = false;
            this.modernButton2.Location = new System.Drawing.Point(95, 45);
            this.modernButton2.Name = "modernButton2";
            this.modernButton2.Size = new System.Drawing.Size(142, 39);
            this.modernButton2.TabIndex = 11;
            this.modernButton2.Text = "WIFI";
            this.modernButton2.UseVisualStyleBackColor = true;
            this.modernButton2.Click += new System.EventHandler(this.ModernButton2_Click);
            // 
            // MainFormMenuOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 189);
            this.Controls.Add(this.shadedPanel1);
            this.Name = "MainFormMenuOption";
            this.Text = "MainFormMenuOption";
            this.Load += new System.EventHandler(this.MainFormMenuOption_Load);
            this.shadedPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private NickAc.ModernUIDoneRight.Controls.ModernButton modernButton2;
        private ShadedPanel shadedPanel1;
        private NickAc.ModernUIDoneRight.Controls.ModernButton modernButton4;
    }
}