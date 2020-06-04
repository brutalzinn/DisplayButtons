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
            this.modernButton3 = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.modernButton2 = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.SuspendLayout();
            // 
            // modernButton3
            // 
            this.modernButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modernButton3.CustomColorScheme = false;
            this.modernButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.modernButton3.Location = new System.Drawing.Point(113, 125);
            this.modernButton3.Name = "modernButton3";
            this.modernButton3.Size = new System.Drawing.Size(142, 39);
            this.modernButton3.TabIndex = 10;
            this.modernButton3.Text = "USB";
            this.modernButton3.UseVisualStyleBackColor = true;
            // 
            // modernButton2
            // 
            this.modernButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modernButton2.CustomColorScheme = false;
            this.modernButton2.Location = new System.Drawing.Point(113, 36);
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
            this.Controls.Add(this.modernButton3);
            this.Controls.Add(this.modernButton2);
            this.Name = "MainFormMenuOption";
            this.Text = "MainFormMenuOption";
            this.Load += new System.EventHandler(this.MainFormMenuOption_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private NickAc.ModernUIDoneRight.Controls.ModernButton modernButton3;
        private NickAc.ModernUIDoneRight.Controls.ModernButton modernButton2;
    }
}