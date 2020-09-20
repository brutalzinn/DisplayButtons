namespace DisplayButtons.Forms.AudioController
{
    partial class RecordDevices
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
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(26, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(402, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // save_button
            // 
            this.save_button.CustomColorScheme = false;
            this.save_button.Image = null;
            this.save_button.Location = new System.Drawing.Point(332, 83);
            this.save_button.Name = "save_button";
            this.save_button.NormalImage = null;
            this.save_button.Origin = null;
            this.save_button.Size = new System.Drawing.Size(96, 39);
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
            this.cancel_button.Location = new System.Drawing.Point(230, 83);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.NormalImage = null;
            this.cancel_button.Origin = null;
            this.cancel_button.Size = new System.Drawing.Size(96, 39);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.TextButton = null;
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // RecordDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 134);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.comboBox1);
            this.Name = "RecordDevices";
            this.Text = "RecordDevices";
            this.Load += new System.EventHandler(this.RecordDevices_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.ImageModernButton save_button;
        private Controls.ImageModernButton cancel_button;
        public System.Windows.Forms.ComboBox comboBox1;
    }
}