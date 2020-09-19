namespace DisplayButtons.Forms.ActionHelperForms.SpotifyForms
{
    partial class SpotifyAudioSensibility
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ok_button = new DisplayButtons.Controls.ImageModernButton();
            this.Cancel_button = new DisplayButtons.Controls.ImageModernButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(43, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 23);
            this.textBox1.TabIndex = 0;
            // 
            // ok_button
            // 
            this.ok_button.CustomColorScheme = false;
            this.ok_button.Image = null;
            this.ok_button.Location = new System.Drawing.Point(205, 178);
            this.ok_button.Name = "ok_button";
            this.ok_button.NormalImage = null;
            this.ok_button.Origin = null;
            this.ok_button.Size = new System.Drawing.Size(76, 41);
            this.ok_button.TabIndex = 1;
            this.ok_button.Text = "OK";
            this.ok_button.TextButton = null;
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // Cancel_button
            // 
            this.Cancel_button.CustomColorScheme = false;
            this.Cancel_button.Image = null;
            this.Cancel_button.Location = new System.Drawing.Point(121, 178);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.NormalImage = null;
            this.Cancel_button.Origin = null;
            this.Cancel_button.Size = new System.Drawing.Size(78, 41);
            this.Cancel_button.TabIndex = 1;
            this.Cancel_button.Text = "CANCEL";
            this.Cancel_button.TextButton = null;
            this.Cancel_button.UseVisualStyleBackColor = true;
            this.Cancel_button.Click += new System.EventHandler(this.Cancel_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // SpotifyAudioSensibility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 232);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.textBox1);
            this.Name = "SpotifyAudioSensibility";
            this.Text = "SpotifyAudioSensibility";
            this.Load += new System.EventHandler(this.SpotifyAudioSensibility_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.ImageModernButton ok_button;
        private Controls.ImageModernButton Cancel_button;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}