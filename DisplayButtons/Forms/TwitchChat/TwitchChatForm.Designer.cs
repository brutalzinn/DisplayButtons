namespace DisplayButtons.Forms.TwitchChat
{
    partial class TwitchChatForm
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
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kickMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.muteMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.banStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(16, 45);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(597, 347);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(16, 406);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(474, 31);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.Camada = 0;
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.ImageLayerTwo = null;
            this.imageModernButton1.Location = new System.Drawing.Point(650, 398);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.NormalLayerTwo = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(128, 40);
            this.imageModernButton1.TabIndex = 2;
            this.imageModernButton1.Text = "OK";
            this.imageModernButton1.TextButton = null;
            this.imageModernButton1.UseVisualStyleBackColor = true;
            this.imageModernButton1.Click += new System.EventHandler(this.imageModernButton1_Click);
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.Camada = 0;
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.ImageLayerTwo = null;
            this.imageModernButton2.Location = new System.Drawing.Point(496, 409);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.NormalLayerTwo = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(57, 28);
            this.imageModernButton2.TabIndex = 3;
            this.imageModernButton2.Text = "Send";
            this.imageModernButton2.TextButton = null;
            this.imageModernButton2.UseVisualStyleBackColor = true;
            this.imageModernButton2.Click += new System.EventHandler(this.imageModernButton2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(622, 47);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(165, 334);
            this.listBox1.TabIndex = 4;
            this.listBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kickMenuItem1,
            this.muteMenuItem1,
            this.banStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 70);
            this.contextMenuStrip1.Text = "Options";
            // 
            // kickMenuItem1
            // 
            this.kickMenuItem1.Name = "kickMenuItem1";
            this.kickMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.kickMenuItem1.Text = "Kick";
            // 
            // muteMenuItem1
            // 
            this.muteMenuItem1.Name = "muteMenuItem1";
            this.muteMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.muteMenuItem1.Text = "muteMenuItem1";
            // 
            // banStripMenuItem1
            // 
            this.banStripMenuItem1.Name = "banStripMenuItem1";
            this.banStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.banStripMenuItem1.Text = "banStripMenuItem1";
            // 
            // TwitchChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.imageModernButton2);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Name = "TwitchChatForm";
            this.Text = "TwitchChatForm";
            this.Load += new System.EventHandler(this.TwitchChatForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private Controls.ImageModernButton imageModernButton1;
        private Controls.ImageModernButton imageModernButton2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kickMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem muteMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem banStripMenuItem1;
    }
}