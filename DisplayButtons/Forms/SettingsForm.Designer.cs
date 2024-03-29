﻿
using BackendAPI.Utils;
    
    namespace DisplayButtons.Forms
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.appBar1 = new NickAc.ModernUIDoneRight.Controls.AppBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.modernButton2 = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.label2 = new System.Windows.Forms.Label();
            this.colorSchemePreviewControl1 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.colorSchemePreviewControl2 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.modernButton1 = new NickAc.ModernUIDoneRight.Controls.ModernButton();
            this.colorSchemePreviewControl3 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.modernShadowPanel1 = new NickAc.ModernUIDoneRight.Controls.ModernShadowPanel();
            this.colorSchemePreviewControl4 = new DisplayButtons.Controls.ColorSchemePreviewControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.modernShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // appBar1
            // 
            this.appBar1.CastShadow = true;
            this.appBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.appBar1.HamburgerButtonSize = 32;
            this.appBar1.IconVisible = false;
            this.appBar1.Location = new System.Drawing.Point(1, 33);
            this.appBar1.Name = "appBar1";
            this.appBar1.OverrideParentText = false;
            this.appBar1.Size = new System.Drawing.Size(807, 50);
            this.appBar1.TabIndex = 0;
            this.appBar1.Text = "DisplayButtons - Settings";
            this.appBar1.TextFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.appBar1.ToolTip = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Device Name";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(17, 122);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(779, 29);
            this.textBox1.TabIndex = 4;
            // 
            // modernButton2
            // 
            this.modernButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modernButton2.CustomColorScheme = false;
            this.modernButton2.Location = new System.Drawing.Point(679, 571);
            this.modernButton2.Name = "modernButton2";
            this.modernButton2.Size = new System.Drawing.Size(117, 39);
            this.modernButton2.TabIndex = 8;
            this.modernButton2.Text = "Save";
            this.modernButton2.UseVisualStyleBackColor = true;
            this.modernButton2.Click += new System.EventHandler(this.ModernButton2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Theme";
            // 
            // colorSchemePreviewControl1
            // 
            this.colorSchemePreviewControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl1.DescriptionText = "Neptune";
            this.colorSchemePreviewControl1.Location = new System.Drawing.Point(7, 8);
            this.colorSchemePreviewControl1.Name = "colorSchemePreviewControl1";
            this.colorSchemePreviewControl1.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl1.TabIndex = 9;
            this.colorSchemePreviewControl1.UnderlyingAppTheme = AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl1.Click += new System.EventHandler(this.ColorScheme_Selected);
            // 
            // colorSchemePreviewControl2
            // 
            this.colorSchemePreviewControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl2.DescriptionText = "Neptune";
            this.colorSchemePreviewControl2.Location = new System.Drawing.Point(195, 8);
            this.colorSchemePreviewControl2.Name = "colorSchemePreviewControl2";
            this.colorSchemePreviewControl2.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl2.TabIndex = 9;
            this.colorSchemePreviewControl2.UnderlyingAppTheme =AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl2.Click += new System.EventHandler(this.ColorScheme_Selected);
            // 
            // modernButton1
            // 
            this.modernButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modernButton1.CustomColorScheme = false;
            this.modernButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.modernButton1.Location = new System.Drawing.Point(556, 571);
            this.modernButton1.Name = "modernButton1";
            this.modernButton1.Size = new System.Drawing.Size(117, 39);
            this.modernButton1.TabIndex = 8;
            this.modernButton1.Text = "Cancel";
            this.modernButton1.UseVisualStyleBackColor = true;
            this.modernButton1.Click += new System.EventHandler(this.ModernButton1_Click);
            // 
            // colorSchemePreviewControl3
            // 
            this.colorSchemePreviewControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl3.DescriptionText = "Neptune";
            this.colorSchemePreviewControl3.Location = new System.Drawing.Point(383, 8);
            this.colorSchemePreviewControl3.Name = "colorSchemePreviewControl3";
            this.colorSchemePreviewControl3.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl3.TabIndex = 9;
            this.colorSchemePreviewControl3.UnderlyingAppTheme = AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl3.Click += new System.EventHandler(this.ColorScheme_Selected);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 446);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "IFTTT Maker Key";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(19, 470);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(779, 29);
            this.textBox2.TabIndex = 11;
            // 
            // modernShadowPanel1
            // 
            this.modernShadowPanel1.Controls.Add(this.colorSchemePreviewControl4);
            this.modernShadowPanel1.Controls.Add(this.colorSchemePreviewControl2);
            this.modernShadowPanel1.Controls.Add(this.colorSchemePreviewControl1);
            this.modernShadowPanel1.Controls.Add(this.colorSchemePreviewControl3);
            this.modernShadowPanel1.Location = new System.Drawing.Point(17, 261);
            this.modernShadowPanel1.Name = "modernShadowPanel1";
            this.modernShadowPanel1.Size = new System.Drawing.Size(779, 182);
            this.modernShadowPanel1.TabIndex = 12;
            this.modernShadowPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.ModernShadowPanel1_Paint_1);
            // 
            // colorSchemePreviewControl4
            // 
            this.colorSchemePreviewControl4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.colorSchemePreviewControl4.DescriptionText = "Neptune";
            this.colorSchemePreviewControl4.Location = new System.Drawing.Point(577, 8);
            this.colorSchemePreviewControl4.Name = "colorSchemePreviewControl4";
            this.colorSchemePreviewControl4.Size = new System.Drawing.Size(177, 167);
            this.colorSchemePreviewControl4.TabIndex = 10;
            this.colorSchemePreviewControl4.UnderlyingAppTheme = AppSettings.AppTheme.Neptune;
            this.colorSchemePreviewControl4.Click += new System.EventHandler(this.ColorScheme_Selected);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "pt-BR",
            "en-US"});
            this.comboBox1.Location = new System.Drawing.Point(21, 193);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(733, 29);
            this.comboBox1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "label4";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.modernButton2;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.modernButton1;
            this.ClientSize = new System.Drawing.Size(809, 623);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.modernButton1);
            this.Controls.Add(this.modernButton2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.appBar1);
            this.Controls.Add(this.modernShadowPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "SettingsForm";
            this.Text = "DisplayButtons - Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.modernShadowPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NickAc.ModernUIDoneRight.Controls.AppBar appBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private NickAc.ModernUIDoneRight.Controls.ModernButton modernButton2;
        private System.Windows.Forms.Label label2;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl1;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl2;
        private NickAc.ModernUIDoneRight.Controls.ModernButton modernButton1;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private NickAc.ModernUIDoneRight.Controls.ModernShadowPanel modernShadowPanel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private Controls.ColorSchemePreviewControl colorSchemePreviewControl4;
    }
}