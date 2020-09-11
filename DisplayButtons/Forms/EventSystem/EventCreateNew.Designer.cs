namespace DisplayButtons.Forms.EventSystem
{
    partial class EventCreateNew
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 285);
            this.panel1.TabIndex = 0;
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(25, 53);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(340, 23);
            this.comboBox.TabIndex = 1;
            this.comboBox.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.Location = new System.Drawing.Point(241, 383);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(128, 49);
            this.imageModernButton1.TabIndex = 2;
            this.imageModernButton1.Text = "SAVE";
            this.imageModernButton1.UseVisualStyleBackColor = true;
            this.imageModernButton1.Click += new System.EventHandler(this.imageModernButton1_Click);
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.Location = new System.Drawing.Point(13, 383);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(127, 48);
            this.imageModernButton2.TabIndex = 3;
            this.imageModernButton2.Text = "CANCEL";
            this.imageModernButton2.UseVisualStyleBackColor = true;
            this.imageModernButton2.Click += new System.EventHandler(this.imageModernButton2_Click);
            // 
            // EventCreateNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 439);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.panel1);
            this.Name = "EventCreateNew";
            this.Text = "EventCreateNew";
            this.Load += new System.EventHandler(this.EventCreateNew_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox comboBox;
        private DisplayButtons.Controls.ImageModernButton imageModernButton1;
        private DisplayButtons.Controls.ImageModernButton imageModernButton2;
    }
}