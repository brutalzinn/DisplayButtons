namespace DisplayButtons.Forms.AudioController
{
    partial class VolumeStepper
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
            this.Volume_textbox = new System.Windows.Forms.TextBox();
            this.ok_button = new DisplayButtons.Controls.ImageModernButton();
            this.cancel_button = new DisplayButtons.Controls.ImageModernButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Volume_textbox
            // 
            this.Volume_textbox.Location = new System.Drawing.Point(57, 85);
            this.Volume_textbox.Name = "Volume_textbox";
            this.Volume_textbox.Size = new System.Drawing.Size(329, 23);
            this.Volume_textbox.TabIndex = 0;
            // 
            // ok_button
            // 
            this.ok_button.Camada = 0;
            this.ok_button.CustomColorScheme = false;
            this.ok_button.Image = null;
            this.ok_button.ImageLayerTwo = null;
            this.ok_button.Location = new System.Drawing.Point(320, 131);
            this.ok_button.Name = "ok_button";
            this.ok_button.NormalImage = null;
            this.ok_button.NormalLayerTwo = null;
            this.ok_button.Origin = null;
            this.ok_button.Size = new System.Drawing.Size(96, 37);
            this.ok_button.TabIndex = 1;
            this.ok_button.Text = "OK";
            this.ok_button.TextButton = null;
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Camada = 0;
            this.cancel_button.CustomColorScheme = false;
            this.cancel_button.Image = null;
            this.cancel_button.ImageLayerTwo = null;
            this.cancel_button.Location = new System.Drawing.Point(218, 131);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.NormalImage = null;
            this.cancel_button.NormalLayerTwo = null;
            this.cancel_button.Origin = null;
            this.cancel_button.Size = new System.Drawing.Size(96, 37);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.TextButton = null;
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // VolumeStepper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 181);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.Volume_textbox);
            this.Name = "VolumeStepper";
            this.Text = "VolumeStepper";
            this.Load += new System.EventHandler(this.VolumeStepper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.ImageModernButton ok_button;
        private Controls.ImageModernButton cancel_button;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox Volume_textbox;
    }
}