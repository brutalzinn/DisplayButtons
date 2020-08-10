namespace DisplayButtons.Forms.FirstSetup
{
    partial class ThemeSelectionPage
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
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.colorSchemePreviewControl2 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.colorSchemePreviewControl1 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.colorSchemePreviewControl3 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.colorSchemePreviewControl4 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(536, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "That\'s why we provide you a small set of themes that you can choose to use.\r\n";
            // 
            // colorSchemePreviewControl2
            // 
            this.colorSchemePreviewControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl2.DescriptionText = "Neptune";
            this.colorSchemePreviewControl2.ForeColor = System.Drawing.Color.White;
            this.colorSchemePreviewControl2.Location = new System.Drawing.Point(204, 72);
            this.colorSchemePreviewControl2.Name = "colorSchemePreviewControl2";
            this.colorSchemePreviewControl2.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl2.TabIndex = 1;
            this.colorSchemePreviewControl2.UnderlyingAppTheme = DisplayButtons.Backend.Utils.AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl2.Click += new System.EventHandler(this.ColorSchemePreviewControl2_Click);
            // 
            // colorSchemePreviewControl1
            // 
            this.colorSchemePreviewControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl1.DescriptionText = "Neptune";
            this.colorSchemePreviewControl1.ForeColor = System.Drawing.Color.White;
            this.colorSchemePreviewControl1.Location = new System.Drawing.Point(21, 72);
            this.colorSchemePreviewControl1.Name = "colorSchemePreviewControl1";
            this.colorSchemePreviewControl1.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl1.TabIndex = 1;
            this.colorSchemePreviewControl1.UnderlyingAppTheme = DisplayButtons.Backend.Utils.AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl1.Click += new System.EventHandler(this.ColorSchemePreviewControl2_Click);
            // 
            // colorSchemePreviewControl3
            // 
            this.colorSchemePreviewControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl3.DescriptionText = "Neptune";
            this.colorSchemePreviewControl3.ForeColor = System.Drawing.Color.White;
            this.colorSchemePreviewControl3.Location = new System.Drawing.Point(387, 72);
            this.colorSchemePreviewControl3.Name = "colorSchemePreviewControl3";
            this.colorSchemePreviewControl3.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl3.TabIndex = 1;
            this.colorSchemePreviewControl3.UnderlyingAppTheme = DisplayButtons.Backend.Utils.AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl3.Click += new System.EventHandler(this.ColorSchemePreviewControl2_Click);
            // 
            // colorSchemePreviewControl4
            // 
            this.colorSchemePreviewControl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl4.DescriptionText = "Neptune";
            this.colorSchemePreviewControl4.ForeColor = System.Drawing.Color.White;
            this.colorSchemePreviewControl4.Location = new System.Drawing.Point(584, 72);
            this.colorSchemePreviewControl4.Name = "colorSchemePreviewControl4";
            this.colorSchemePreviewControl4.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl4.TabIndex = 2;
            this.colorSchemePreviewControl4.UnderlyingAppTheme = DisplayButtons.Backend.Utils.AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl4.Click += new System.EventHandler(this.ColorSchemePreviewControl2_Click);
            // 
            // ThemeSelectionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.colorSchemePreviewControl4);
            this.Controls.Add(this.colorSchemePreviewControl1);
            this.Controls.Add(this.colorSchemePreviewControl3);
            this.Controls.Add(this.colorSchemePreviewControl2);
            this.Controls.Add(this.label4);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximumSize = new System.Drawing.Size(800, 244);
            this.MinimumSize = new System.Drawing.Size(800, 244);
            this.Name = "ThemeSelectionPage";
            this.Size = new System.Drawing.Size(800, 244);
            this.Load += new System.EventHandler(this.ThemeSelectionPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl2;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl1;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl3;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl4;
    }
}
