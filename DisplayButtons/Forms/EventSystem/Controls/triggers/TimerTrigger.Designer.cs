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
            this.interval_label = new System.Windows.Forms.Label();
            this.datehour_label = new System.Windows.Forms.Label();
            this.datetime_groupbox = new System.Windows.Forms.GroupBox();
            this.interval_groupbox = new System.Windows.Forms.GroupBox();
            this.datetime_groupbox.SuspendLayout();
            this.interval_groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd-MM-yyyy HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(84, 20);
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
            this.textBox1.Location = new System.Drawing.Point(69, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 23);
            this.textBox1.TabIndex = 5;
            // 
            // interval_label
            // 
            this.interval_label.AutoSize = true;
            this.interval_label.Location = new System.Drawing.Point(10, 26);
            this.interval_label.Name = "interval_label";
            this.interval_label.Size = new System.Drawing.Size(53, 15);
            this.interval_label.TabIndex = 6;
            this.interval_label.Text = "Intervalo";
            // 
            // datehour_label
            // 
            this.datehour_label.AutoSize = true;
            this.datehour_label.Location = new System.Drawing.Point(1, 26);
            this.datehour_label.Name = "datehour_label";
            this.datehour_label.Size = new System.Drawing.Size(62, 15);
            this.datehour_label.TabIndex = 7;
            this.datehour_label.Text = "Hora/Data";
            // 
            // datetime_groupbox
            // 
            this.datetime_groupbox.Controls.Add(this.datehour_label);
            this.datetime_groupbox.Controls.Add(this.dateTimePicker1);
            this.datetime_groupbox.Location = new System.Drawing.Point(15, 68);
            this.datetime_groupbox.Name = "datetime_groupbox";
            this.datetime_groupbox.Size = new System.Drawing.Size(291, 60);
            this.datetime_groupbox.TabIndex = 8;
            this.datetime_groupbox.TabStop = false;
            this.datetime_groupbox.Text = "Date/hour control";
            // 
            // interval_groupbox
            // 
            this.interval_groupbox.Controls.Add(this.textBox1);
            this.interval_groupbox.Controls.Add(this.interval_label);
            this.interval_groupbox.Location = new System.Drawing.Point(15, 134);
            this.interval_groupbox.Name = "interval_groupbox";
            this.interval_groupbox.Size = new System.Drawing.Size(291, 55);
            this.interval_groupbox.TabIndex = 9;
            this.interval_groupbox.TabStop = false;
            this.interval_groupbox.Text = "Interval";
            // 
            // TimerTrigger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.interval_groupbox);
            this.Controls.Add(this.datetime_groupbox);
            this.Controls.Add(this.recurring_timer_radio);
            this.Controls.Add(this.Datetime_radio);
            this.Name = "TimerTrigger";
            this.Size = new System.Drawing.Size(340, 219);
            this.Load += new System.EventHandler(this.TimerTrigger_Load_1);
            this.datetime_groupbox.ResumeLayout(false);
            this.datetime_groupbox.PerformLayout();
            this.interval_groupbox.ResumeLayout(false);
            this.interval_groupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.RadioButton Datetime_radio;
        public System.Windows.Forms.RadioButton recurring_timer_radio;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label interval_label;
        private System.Windows.Forms.Label datehour_label;
        private System.Windows.Forms.GroupBox datetime_groupbox;
        private System.Windows.Forms.GroupBox interval_groupbox;
    }
}
