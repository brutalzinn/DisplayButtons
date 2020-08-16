namespace DisplayButtons.Forms.EventSystem.Controls.EventCreator.Controls
{
    partial class trigger_control_new
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(16, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(330, 23);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(14, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 242);
            this.panel1.TabIndex = 1;
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.Location = new System.Drawing.Point(237, 301);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(108, 54);
            this.imageModernButton1.TabIndex = 2;
            this.imageModernButton1.Text = "SAVE";
            this.imageModernButton1.UseVisualStyleBackColor = true;
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.Location = new System.Drawing.Point(16, 301);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(108, 54);
            this.imageModernButton2.TabIndex = 2;
            this.imageModernButton2.Text = "CANCEL";
            this.imageModernButton2.UseVisualStyleBackColor = true;
            // 
            // trigger_control_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBox1);
            this.Name = "trigger_control_new";
            this.Size = new System.Drawing.Size(361, 367);
            this.Load += new System.EventHandler(this.trigger_control_new_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private DisplayButtons.Controls.ImageModernButton imageModernButton1;
        private DisplayButtons.Controls.ImageModernButton imageModernButton2;
    }
}
