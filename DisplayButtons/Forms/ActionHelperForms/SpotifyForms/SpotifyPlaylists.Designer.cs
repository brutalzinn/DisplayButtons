namespace DisplayButtons.Forms.ActionHelperForms.SpotifyForms
{
    partial class SpotifyPlaylists
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.save_button = new DisplayButtons.Controls.ImageModernButton();
            this.cancel_button = new DisplayButtons.Controls.ImageModernButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(397, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // save_button
            // 
            this.save_button.CustomColorScheme = false;
            this.save_button.Image = null;
            this.save_button.Location = new System.Drawing.Point(319, 136);
            this.save_button.Name = "save_button";
            this.save_button.NormalImage = null;
            this.save_button.Origin = null;
            this.save_button.Size = new System.Drawing.Size(91, 41);
            this.save_button.TabIndex = 1;
            this.save_button.Text = "OK";
            this.save_button.TextButton = null;
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.CustomColorScheme = false;
            this.cancel_button.Image = null;
            this.cancel_button.Location = new System.Drawing.Point(222, 136);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.NormalImage = null;
            this.cancel_button.Origin = null;
            this.cancel_button.Size = new System.Drawing.Size(91, 41);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.TextButton = null;
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // SpotifyPlaylists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 196);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.comboBox1);
            this.Name = "SpotifyPlaylists";
            this.Text = "SpotifyPlaylists";
            this.Load += new System.EventHandler(this.SpotifyPlaylists_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.ImageModernButton save_button;
        private Controls.ImageModernButton cancel_button;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}