namespace DisplayButtons.Forms.EventSystem.Controls.triggers
{
    partial class TimerTrigger
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Datetime_radio = new System.Windows.Forms.RadioButton();
            this.recurring_timer_radio = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(99, 86);
            this.dateTimePicker1.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(2001, 12, 12, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(194, 23);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.Value = new System.DateTime(2020, 8, 29, 0, 0, 0, 0);
            // 
            // Datetime_radio
            // 
            this.Datetime_radio.AutoSize = true;
            this.Datetime_radio.Location = new System.Drawing.Point(82, 47);
            this.Datetime_radio.Name = "Datetime_radio";
            this.Datetime_radio.Size = new System.Drawing.Size(75, 19);
            this.Datetime_radio.TabIndex = 4;
            this.Datetime_radio.TabStop = true;
            this.Datetime_radio.Text = "DateTime";
            this.Datetime_radio.UseVisualStyleBackColor = true;
            this.Datetime_radio.CheckedChanged += new System.EventHandler(this.recurring_timer_radio_CheckedChanged);
            // 
            // recurring_timer_radio
            // 
            this.recurring_timer_radio.AutoSize = true;
            this.recurring_timer_radio.Location = new System.Drawing.Point(201, 47);
            this.recurring_timer_radio.Name = "recurring_timer_radio";
            this.recurring_timer_radio.Size = new System.Drawing.Size(105, 19);
            this.recurring_timer_radio.TabIndex = 4;
            this.recurring_timer_radio.TabStop = true;
            this.recurring_timer_radio.Text = "Recurring Time";
            this.recurring_timer_radio.UseVisualStyleBackColor = true;
            this.recurring_timer_radio.CheckedChanged += new System.EventHandler(this.recurring_timer_radio_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(99, 128);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 23);
            this.textBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Intervalo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hora/Data";
            // 
            // TimerTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.recurring_timer_radio);
            this.Controls.Add(this.Datetime_radio);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "TimerTrigger";
            this.Size = new System.Drawing.Size(340, 188);
            this.Load += new System.EventHandler(this.TimerTrigger_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.RadioButton Datetime_radio;
        public System.Windows.Forms.RadioButton recurring_timer_radio;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
