namespace DisplayButtons.Forms.EventSystem.Controls
{
    partial class conditions_user_control
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_interval_radio = new System.Windows.Forms.RadioButton();
            this.timer_exact_radio = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.timer_exact_radio);
            this.groupBox1.Controls.Add(this.timer_interval_radio);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timer condition";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(6, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 92);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scripts condition";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(6, 249);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(618, 54);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Application condition";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 23);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lua";
            // 
            // timer_interval_radio
            // 
            this.timer_interval_radio.AutoSize = true;
            this.timer_interval_radio.Location = new System.Drawing.Point(6, 22);
            this.timer_interval_radio.Name = "timer_interval_radio";
            this.timer_interval_radio.Size = new System.Drawing.Size(97, 19);
            this.timer_interval_radio.TabIndex = 0;
            this.timer_interval_radio.TabStop = true;
            this.timer_interval_radio.Text = "Timer interval";
            this.timer_interval_radio.UseVisualStyleBackColor = true;
            this.timer_interval_radio.CheckedChanged += new System.EventHandler(this.timer_interval_radio_CheckedChanged);
            // 
            // timer_exact_radio
            // 
            this.timer_exact_radio.AutoSize = true;
            this.timer_exact_radio.Location = new System.Drawing.Point(6, 47);
            this.timer_exact_radio.Name = "timer_exact_radio";
            this.timer_exact_radio.Size = new System.Drawing.Size(86, 19);
            this.timer_exact_radio.TabIndex = 0;
            this.timer_exact_radio.TabStop = true;
            this.timer_exact_radio.Text = "Timer exact";
            this.timer_exact_radio.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 70);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(82, 19);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Timer after";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(131, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 85);
            this.panel1.TabIndex = 1;
            // 
            // conditions_user_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "conditions_user_control";
            this.Size = new System.Drawing.Size(640, 315);
            this.Load += new System.EventHandler(this.conditions_user_control_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton timer_exact_radio;
        private System.Windows.Forms.RadioButton timer_interval_radio;
        private System.Windows.Forms.Panel panel1;
    }
}
