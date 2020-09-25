namespace DisplayButtons.Forms.TwitchChat
{
    partial class TwitchChatForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(16, 45);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(771, 347);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(16, 406);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(474, 31);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.Camada = 0;
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.ImageLayerTwo = null;
            this.imageModernButton1.Location = new System.Drawing.Point(650, 398);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.NormalLayerTwo = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(128, 40);
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
            this.imageModernButton2.Location = new System.Drawing.Point(496, 409);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.NormalLayerTwo = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(57, 28);
            this.imageModernButton2.TabIndex = 3;
            this.imageModernButton2.Text = "Send";
            this.imageModernButton2.TextButton = null;
            this.imageModernButton2.UseVisualStyleBackColor = true;
            // 
            // TwitchChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Name = "TwitchChatForm";
            this.Text = "TwitchChatForm";
            this.Load += new System.EventHandler(this.TwitchChatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private Controls.ImageModernButton imageModernButton1;
        private Controls.ImageModernButton imageModernButton2;
    }
}