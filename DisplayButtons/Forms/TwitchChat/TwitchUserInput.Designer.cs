namespace DisplayButtons.Forms.TwitchChat
{
    partial class TwitchUserInput
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
            this.timer_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.motivo_ritchtextbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // timer_textbox
            // 
            this.timer_textbox.Location = new System.Drawing.Point(30, 77);
            this.timer_textbox.Name = "timer_textbox";
            this.timer_textbox.Size = new System.Drawing.Size(388, 23);
            this.timer_textbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.Camada = 0;
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.ImageLayerTwo = null;
            this.imageModernButton1.Location = new System.Drawing.Point(323, 203);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.NormalLayerTwo = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(95, 40);
            this.imageModernButton1.TabIndex = 2;
            this.imageModernButton1.Text = "OK";
            this.imageModernButton1.TextButton = null;
            this.imageModernButton1.UseVisualStyleBackColor = true;
            this.imageModernButton1.Click += new System.EventHandler(this.imageModernButton1_Click);
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.Camada = 0;
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.ImageLayerTwo = null;
            this.imageModernButton2.Location = new System.Drawing.Point(222, 203);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.NormalLayerTwo = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(95, 40);
            this.imageModernButton2.TabIndex = 2;
            this.imageModernButton2.Text = "CANCEL";
            this.imageModernButton2.TextButton = null;
            this.imageModernButton2.UseVisualStyleBackColor = true;
            this.imageModernButton2.Click += new System.EventHandler(this.imageModernButton2_Click);
            // 
            // motivo_ritchtextbox
            // 
            this.motivo_ritchtextbox.Location = new System.Drawing.Point(30, 125);
            this.motivo_ritchtextbox.Name = "motivo_ritchtextbox";
            this.motivo_ritchtextbox.Size = new System.Drawing.Size(388, 72);
            this.motivo_ritchtextbox.TabIndex = 3;
            this.motivo_ritchtextbox.Text = "";
            // 
            // TwitchUserInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 256);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.motivo_ritchtextbox);
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timer_textbox);
            this.Name = "TwitchUserInput";
            this.Text = "Twitch Mute User";
            this.Load += new System.EventHandler(this.TwitchUserInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Controls.ImageModernButton imageModernButton1;
        private Controls.ImageModernButton imageModernButton2;
        public System.Windows.Forms.TextBox timer_textbox;
        public System.Windows.Forms.RichTextBox motivo_ritchtextbox;
    }
}