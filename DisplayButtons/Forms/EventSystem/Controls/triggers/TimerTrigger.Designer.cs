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
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd-MM-yyyy hh:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(47, 118);
            this.dateTimePicker1.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker1.MinDate = new System.DateTime(2001, 12, 12, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(246, 23);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.Value = new System.DateTime(2020, 8, 29, 0, 0, 0, 0);
            // 
            // Datetime_radio
            // 
            this.Datetime_radio.AutoSize = true;
            this.Datetime_radio.Location = new System.Drawing.Point(67, 59);
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
            this.recurring_timer_radio.Location = new System.Drawing.Point(188, 59);
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
            this.textBox1.Location = new System.Drawing.Point(67, 89);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(222, 23);
            this.textBox1.TabIndex = 5;
            // 
            // TimerTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.recurring_timer_radio);
            this.Controls.Add(this.Datetime_radio);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "TimerTrigger";
            this.Size = new System.Drawing.Size(327, 237);
            this.Load += new System.EventHandler(this.TimerTrigger_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.RadioButton Datetime_radio;
        public System.Windows.Forms.RadioButton recurring_timer_radio;
        public System.Windows.Forms.TextBox textBox1;
    }
}
