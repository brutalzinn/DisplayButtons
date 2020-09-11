namespace DisplayButtons.Forms.EventSystem
{
    partial class Events
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton3 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton4 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton5 = new DisplayButtons.Controls.ImageModernButton();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(1, 33);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(713, 289);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.Location = new System.Drawing.Point(161, 337);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(124, 57);
            this.imageModernButton1.TabIndex = 1;
            this.imageModernButton1.Text = "Delete";
            this.imageModernButton1.UseVisualStyleBackColor = true;
            this.imageModernButton1.Click += new System.EventHandler(this.imageModernButton1_Click);
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.Location = new System.Drawing.Point(31, 337);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(124, 57);
            this.imageModernButton2.TabIndex = 1;
            this.imageModernButton2.Text = "Back";
            this.imageModernButton2.UseVisualStyleBackColor = true;
            this.imageModernButton2.Click += new System.EventHandler(this.imageModernButton2_Click);
            // 
            // imageModernButton3
            // 
            this.imageModernButton3.CustomColorScheme = false;
            this.imageModernButton3.Image = null;
            this.imageModernButton3.Location = new System.Drawing.Point(551, 337);
            this.imageModernButton3.Name = "imageModernButton3";
            this.imageModernButton3.NormalImage = null;
            this.imageModernButton3.Origin = null;
            this.imageModernButton3.Size = new System.Drawing.Size(124, 57);
            this.imageModernButton3.TabIndex = 1;
            this.imageModernButton3.Text = "New";
            this.imageModernButton3.UseVisualStyleBackColor = true;
            this.imageModernButton3.Click += new System.EventHandler(this.imageModernButton3_Click);
            // 
            // imageModernButton4
            // 
            this.imageModernButton4.CustomColorScheme = false;
            this.imageModernButton4.Image = null;
            this.imageModernButton4.Location = new System.Drawing.Point(421, 337);
            this.imageModernButton4.Name = "imageModernButton4";
            this.imageModernButton4.NormalImage = null;
            this.imageModernButton4.Origin = null;
            this.imageModernButton4.Size = new System.Drawing.Size(124, 57);
            this.imageModernButton4.TabIndex = 1;
            this.imageModernButton4.Text = "Config";
            this.imageModernButton4.UseVisualStyleBackColor = true;
            this.imageModernButton4.Click += new System.EventHandler(this.imageModernButton4_Click);
            // 
            // imageModernButton5
            // 
            this.imageModernButton5.CustomColorScheme = false;
            this.imageModernButton5.Image = null;
            this.imageModernButton5.Location = new System.Drawing.Point(291, 337);
            this.imageModernButton5.Name = "imageModernButton5";
            this.imageModernButton5.NormalImage = null;
            this.imageModernButton5.Origin = null;
            this.imageModernButton5.Size = new System.Drawing.Size(124, 57);
            this.imageModernButton5.TabIndex = 2;
            this.imageModernButton5.Text = "Refresh Events";
            this.imageModernButton5.UseVisualStyleBackColor = true;
            this.imageModernButton5.Click += new System.EventHandler(this.imageModernButton5_Click);
            // 
            // Events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 409);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.imageModernButton5);
            this.Controls.Add(this.imageModernButton4);
            this.Controls.Add(this.imageModernButton3);
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.listBox1);
            this.Name = "Events";
            this.Text = "Events";
            this.Load += new System.EventHandler(this.Events_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private DisplayButtons.Controls.ImageModernButton imageModernButton1;
        private DisplayButtons.Controls.ImageModernButton imageModernButton2;
        private DisplayButtons.Controls.ImageModernButton imageModernButton3;
        private DisplayButtons.Controls.ImageModernButton imageModernButton4;
        private DisplayButtons.Controls.ImageModernButton imageModernButton5;
    }
}