namespace DisplayButtons.Forms.EventSystem
{
    partial class event_interface
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_geral_button = new System.Windows.Forms.TabPage();
            this.tab_trigger_button = new System.Windows.Forms.TabPage();
            this.tab_actions_button = new System.Windows.Forms.TabPage();
            this.general_user_control1 = new DisplayButtons.Forms.EventSystem.Controls.general_user_control();
            this.trigger_user_control1 = new DisplayButtons.Forms.EventSystem.Controls.trigger_user_control();
            this.actions_user_control1 = new DisplayButtons.Forms.EventSystem.Controls.actions_user_control();
            this.tabControl1.SuspendLayout();
            this.tab_geral_button.SuspendLayout();
            this.tab_trigger_button.SuspendLayout();
            this.tab_actions_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_geral_button);
            this.tabControl1.Controls.Add(this.tab_trigger_button);
            this.tabControl1.Controls.Add(this.tab_actions_button);
            this.tabControl1.Location = new System.Drawing.Point(15, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 2;
            this.tabControl1.Size = new System.Drawing.Size(653, 332);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tab_geral_button
            // 
            this.tab_geral_button.Controls.Add(this.general_user_control1);
            this.tab_geral_button.Location = new System.Drawing.Point(4, 24);
            this.tab_geral_button.Name = "tab_geral_button";
            this.tab_geral_button.Padding = new System.Windows.Forms.Padding(3);
            this.tab_geral_button.Size = new System.Drawing.Size(645, 304);
            this.tab_geral_button.TabIndex = 0;
            this.tab_geral_button.Text = "Geral";
            this.tab_geral_button.UseVisualStyleBackColor = true;
            // 
            // tab_trigger_button
            // 
            this.tab_trigger_button.Controls.Add(this.trigger_user_control1);
            this.tab_trigger_button.Location = new System.Drawing.Point(4, 24);
            this.tab_trigger_button.Name = "tab_trigger_button";
            this.tab_trigger_button.Padding = new System.Windows.Forms.Padding(3);
            this.tab_trigger_button.Size = new System.Drawing.Size(645, 304);
            this.tab_trigger_button.TabIndex = 1;
            this.tab_trigger_button.Text = "Disparadores";
            this.tab_trigger_button.UseVisualStyleBackColor = true;
            // 
            // tab_actions_button
            // 
            this.tab_actions_button.Controls.Add(this.actions_user_control1);
            this.tab_actions_button.Location = new System.Drawing.Point(4, 24);
            this.tab_actions_button.Name = "tab_actions_button";
            this.tab_actions_button.Padding = new System.Windows.Forms.Padding(3);
            this.tab_actions_button.Size = new System.Drawing.Size(645, 304);
            this.tab_actions_button.TabIndex = 2;
            this.tab_actions_button.Text = "Ações";
            this.tab_actions_button.UseVisualStyleBackColor = true;
            // 
            // general_user_control1
            // 
            this.general_user_control1.Location = new System.Drawing.Point(1, 3);
            this.general_user_control1.Name = "general_user_control1";
            this.general_user_control1.Size = new System.Drawing.Size(643, 300);
            this.general_user_control1.TabIndex = 0;
            // 
            // trigger_user_control1
            // 
            this.trigger_user_control1.Location = new System.Drawing.Point(4, 0);
            this.trigger_user_control1.Name = "trigger_user_control1";
            this.trigger_user_control1.Size = new System.Drawing.Size(644, 303);
            this.trigger_user_control1.TabIndex = 0;
            // 
            // actions_user_control1
            // 
            this.actions_user_control1.Location = new System.Drawing.Point(3, 3);
            this.actions_user_control1.Name = "actions_user_control1";
            this.actions_user_control1.Size = new System.Drawing.Size(642, 298);
            this.actions_user_control1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 450);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.tabControl1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_geral_button.ResumeLayout(false);
            this.tab_trigger_button.ResumeLayout(false);
            this.tab_actions_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_geral_button;
        private System.Windows.Forms.TabPage tab_trigger_button;
        private System.Windows.Forms.TabPage tab_actions_button;
        private Controls.general_user_control general_user_control1;
        private Controls.trigger_user_control trigger_user_control1;
        private Controls.actions_user_control actions_user_control1;
    }
}