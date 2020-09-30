namespace DisplayButtons.Forms.TwitchChat.configs
{
    partial class TwitchChatConfig
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
            this.checkbox_topmost = new System.Windows.Forms.CheckBox();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.SuspendLayout();
            // 
            // checkbox_topmost
            // 
            this.checkbox_topmost.AutoSize = true;
            this.checkbox_topmost.Location = new System.Drawing.Point(87, 117);
            this.checkbox_topmost.Name = "checkbox_topmost";
            this.checkbox_topmost.Size = new System.Drawing.Size(75, 19);
            this.checkbox_topmost.TabIndex = 0;
            this.checkbox_topmost.Text = "Top most";
            this.checkbox_topmost.UseVisualStyleBackColor = true;
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(87, 75);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(209, 23);
            this.username_textbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.Camada = 0;
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.ImageLayerTwo = null;
            this.imageModernButton1.Location = new System.Drawing.Point(214, 206);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.NormalLayerTwo = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(82, 44);
            this.imageModernButton1.TabIndex = 3;
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
            this.imageModernButton2.Location = new System.Drawing.Point(126, 206);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.NormalLayerTwo = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(82, 44);
            this.imageModernButton2.TabIndex = 3;
            this.imageModernButton2.Text = "CANCEL";
            this.imageModernButton2.TextButton = null;
            this.imageModernButton2.UseVisualStyleBackColor = true;
            this.imageModernButton2.Click += new System.EventHandler(this.imageModernButton2_Click);
            // 
            // TwitchChatConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 263);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.username_textbox);
            this.Controls.Add(this.checkbox_topmost);
            this.Name = "TwitchChatConfig";
            this.Text = "TwitchChatConfig";
            this.Load += new System.EventHandler(this.TwitchChatConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox checkbox_topmost;
        public System.Windows.Forms.TextBox username_textbox;
        private System.Windows.Forms.Label label1;
        private Controls.ImageModernButton imageModernButton1;
        private Controls.ImageModernButton imageModernButton2;
    }
}