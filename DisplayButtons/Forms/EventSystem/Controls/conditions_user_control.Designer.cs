﻿namespace DisplayButtons.Forms.EventSystem.Controls
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
            this.timer_before_radio = new System.Windows.Forms.RadioButton();
            this.timer_none_radio_button = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.time_after_radio = new System.Windows.Forms.RadioButton();
            this.timer_exact_radio = new System.Windows.Forms.RadioButton();
            this.timer_interval_radio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timer_before_radio);
            this.groupBox1.Controls.Add(this.timer_none_radio_button);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.time_after_radio);
            this.groupBox1.Controls.Add(this.timer_exact_radio);
            this.groupBox1.Controls.Add(this.timer_interval_radio);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timer condition";
            // 
            // timer_before_radio
            // 
            this.timer_before_radio.AutoSize = true;
            this.timer_before_radio.Location = new System.Drawing.Point(5, 91);
            this.timer_before_radio.Name = "timer_before_radio";
            this.timer_before_radio.Size = new System.Drawing.Size(92, 19);
            this.timer_before_radio.TabIndex = 0;
            this.timer_before_radio.TabStop = true;
            this.timer_before_radio.Text = "Timer before";
            this.timer_before_radio.UseVisualStyleBackColor = true;
            this.timer_before_radio.CheckedChanged += new System.EventHandler(this.timer_interval_radio_CheckedChanged);
            // 
            // timer_none_radio_button
            // 
            this.timer_none_radio_button.AutoSize = true;
            this.timer_none_radio_button.Location = new System.Drawing.Point(6, 113);
            this.timer_none_radio_button.Name = "timer_none_radio_button";
            this.timer_none_radio_button.Size = new System.Drawing.Size(54, 19);
            this.timer_none_radio_button.TabIndex = 0;
            this.timer_none_radio_button.TabStop = true;
            this.timer_none_radio_button.Text = "None";
            this.timer_none_radio_button.UseVisualStyleBackColor = true;
            this.timer_none_radio_button.CheckedChanged += new System.EventHandler(this.timer_interval_radio_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(131, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 104);
            this.panel1.TabIndex = 1;
            // 
            // time_after_radio
            // 
            this.time_after_radio.AutoSize = true;
            this.time_after_radio.Location = new System.Drawing.Point(6, 70);
            this.time_after_radio.Name = "time_after_radio";
            this.time_after_radio.Size = new System.Drawing.Size(82, 19);
            this.time_after_radio.TabIndex = 0;
            this.time_after_radio.TabStop = true;
            this.time_after_radio.Text = "Timer after";
            this.time_after_radio.UseVisualStyleBackColor = true;
            this.time_after_radio.CheckedChanged += new System.EventHandler(this.timer_interval_radio_CheckedChanged);
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
            this.timer_exact_radio.CheckedChanged += new System.EventHandler(this.timer_interval_radio_CheckedChanged);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(6, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 92);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scripts condition";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lua";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 23);
            this.textBox1.TabIndex = 0;
            // 
            // conditions_user_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
        private System.Windows.Forms.RadioButton time_after_radio;
        private System.Windows.Forms.RadioButton timer_exact_radio;
        private System.Windows.Forms.RadioButton timer_interval_radio;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton timer_none_radio_button;
        private System.Windows.Forms.RadioButton timer_before_radio;
    }
}
