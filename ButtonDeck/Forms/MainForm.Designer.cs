using ButtonDeck.Controls;
using System;
using System.Windows.Forms;

namespace ButtonDeck.Forms
{
    partial class MainForm
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
            if (disposing && (components != null)) {
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
            this.appBar1 = new NickAc.ModernUIDoneRight.Controls.AppBar();
            this.panel1 = new NickAc.ModernUIDoneRight.Controls.ModernShadowPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.shadedPanel1 = new ButtonDeck.Forms.ShadedPanel();
            this.shadedPanel2 = new ButtonDeck.Forms.ShadedPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.imageModernButton1 = new ButtonDeck.Controls.ImageModernButton();
            this.button1 = new System.Windows.Forms.Button();
            this.shadedPanel1.SuspendLayout();
            this.shadedPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // appBar1
            // 
            this.appBar1.CastShadow = true;
            this.appBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.appBar1.HamburgerButtonSize = 32;
            this.appBar1.IconVisible = false;
            this.appBar1.Location = new System.Drawing.Point(1, 33);
            this.appBar1.Name = "appBar1";
            this.appBar1.OverrideParentText = false;
            this.appBar1.Size = new System.Drawing.Size(924, 50);
            this.appBar1.TabIndex = 0;
            this.appBar1.Text = "ButtonDeck";
            this.appBar1.TextFont = new System.Drawing.Font("Segoe UI", 14F);
            this.appBar1.Click += new System.EventHandler(this.appBar1_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(1, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 506);
            this.panel1.TabIndex = 2;
            this.panel1.Click += new System.EventHandler(this.Buttons_Unfocus);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 499);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(424, 69);
            this.label1.TabIndex = 3;
            this.label1.Text = "The buttons will appear after you connect the device.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // shadedPanel1
            // 
            this.shadedPanel1.Controls.Add(this.shadedPanel2);
            this.shadedPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.shadedPanel1.Location = new System.Drawing.Point(669, 83);
            this.shadedPanel1.Name = "shadedPanel1";
            this.shadedPanel1.Size = new System.Drawing.Size(256, 506);
            this.shadedPanel1.TabIndex = 4;
            this.shadedPanel1.Click += new System.EventHandler(this.Buttons_Unfocus);
            // 
            // shadedPanel2
            // 
            this.shadedPanel2.ColorScheme = null;
            this.shadedPanel2.Controls.Add(this.flowLayoutPanel1);
            this.shadedPanel2.Controls.Add(this.label2);
            this.shadedPanel2.Controls.Add(this.imageModernButton1);
            this.shadedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.shadedPanel2.Location = new System.Drawing.Point(0, 244);
            this.shadedPanel2.Name = "shadedPanel2";
            this.shadedPanel2.Size = new System.Drawing.Size(256, 262);
            this.shadedPanel2.TabIndex = 0;
            this.shadedPanel2.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(15, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(229, 150);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(102, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 36);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action Text";
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.Location = new System.Drawing.Point(15, 13);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(80, 80);
            this.imageModernButton1.TabIndex = 2;
            this.imageModernButton1.UseVisualStyleBackColor = true;
            this.imageModernButton1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImageModernButton1_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(386, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(926, 590);
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.shadedPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.appBar1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(915, 535);
            this.Name = "MainForm";
            this.Tag = "";
            this.Text = "ButtonDeck";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.Buttons_Unfocus);
            this.shadedPanel1.ResumeLayout(false);
            this.shadedPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NickAc.ModernUIDoneRight.Controls.AppBar appBar1;
        private NickAc.ModernUIDoneRight.Controls.ModernShadowPanel panel1;
        private System.Windows.Forms.Label label1;
        private ShadedPanel shadedPanel2;
        private ImageModernButton imageModernButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ShadedPanel shadedPanel1;
        private Button button1;

        internal ShadedPanel ShadedPanel1 { get => shadedPanel1; set => shadedPanel1 = value; }
    }
}