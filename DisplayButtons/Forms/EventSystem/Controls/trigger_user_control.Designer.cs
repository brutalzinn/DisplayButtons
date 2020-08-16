namespace DisplayButtons.Forms.EventSystem.Controls
{
    partial class trigger_user_control
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
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton3 = new DisplayButtons.Controls.ImageModernButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.Location = new System.Drawing.Point(505, 295);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(130, 44);
            this.imageModernButton1.TabIndex = 1;
            this.imageModernButton1.Text = "Novo";
            this.imageModernButton1.UseVisualStyleBackColor = true;
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.Location = new System.Drawing.Point(32, 295);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(130, 44);
            this.imageModernButton2.TabIndex = 1;
            this.imageModernButton2.Text = "Remover";
            this.imageModernButton2.UseVisualStyleBackColor = true;
            // 
            // imageModernButton3
            // 
            this.imageModernButton3.CustomColorScheme = false;
            this.imageModernButton3.Image = null;
            this.imageModernButton3.Location = new System.Drawing.Point(259, 295);
            this.imageModernButton3.Name = "imageModernButton3";
            this.imageModernButton3.NormalImage = null;
            this.imageModernButton3.Origin = null;
            this.imageModernButton3.Size = new System.Drawing.Size(130, 44);
            this.imageModernButton3.TabIndex = 1;
            this.imageModernButton3.Text = "Configurar";
            this.imageModernButton3.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(660, 274);
            this.listBox1.TabIndex = 2;
            // 
            // trigger_user_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.imageModernButton3);
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Name = "trigger_user_control";
            this.Size = new System.Drawing.Size(660, 348);
            this.ResumeLayout(false);

        }

        #endregion
        private DisplayButtons.Controls.ImageModernButton imageModernButton1;
        private DisplayButtons.Controls.ImageModernButton imageModernButton2;
        private DisplayButtons.Controls.ImageModernButton imageModernButton3;
        private System.Windows.Forms.ListBox listBox1;
    }
}
