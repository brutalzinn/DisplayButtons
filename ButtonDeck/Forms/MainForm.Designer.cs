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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.appBar1 = new NickAc.ModernUIDoneRight.Controls.AppBar();
            this.panel1 = new NickAc.ModernUIDoneRight.Controls.ModernShadowPanel();
            this.warning_label = new System.Windows.Forms.Label();
            this.shadedPanel1 = new ButtonDeck.Forms.ShadedPanel();
            this.shadedPanel2 = new ButtonDeck.Forms.ShadedPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.action_label = new System.Windows.Forms.Label();
            this.imageModernButton1 = new ButtonDeck.Controls.ImageModernButton();
            this.painel_developer = new ButtonDeck.Forms.ShadedPanel();
            this.imageModernButton5 = new ButtonDeck.Controls.ImageModernButton();
            this.imageModernButton4 = new ButtonDeck.Controls.ImageModernButton();
            this.imageModernButton3 = new ButtonDeck.Controls.ImageModernButton();
            this.imageModernButton2 = new ButtonDeck.Controls.ImageModernButton();
            this.shadedPanel4 = new ButtonDeck.Forms.ShadedPanel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.shadedPanel1.SuspendLayout();
            this.shadedPanel2.SuspendLayout();
            this.painel_developer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.appBar1.Size = new System.Drawing.Size(1091, 50);
            this.appBar1.TabIndex = 0;
            this.appBar1.Text = "ButtonDeck";
            this.appBar1.TextFont = new System.Drawing.Font("Segoe UI", 14F);
            this.appBar1.ToolTip = null;
            this.appBar1.Click += new System.EventHandler(this.appBar1_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.warning_label);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(141, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 450);
            this.panel1.TabIndex = 2;
            this.panel1.Click += new System.EventHandler(this.Buttons_Unfocus);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.Resize += new System.EventHandler(this.Panel1_Resize);
            // 
            // warning_label
            // 
            this.warning_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.warning_label.BackColor = System.Drawing.Color.Transparent;
            this.warning_label.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warning_label.Location = new System.Drawing.Point(138, 203);
            this.warning_label.Name = "warning_label";
            this.warning_label.Size = new System.Drawing.Size(424, 69);
            this.warning_label.TabIndex = 3;
            this.warning_label.Text = "The buttons will appear after you connect the device.";
            this.warning_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warning_label.Visible = false;
            // 
            // shadedPanel1
            // 
            this.shadedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadedPanel1.AutoSize = true;
            this.shadedPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.shadedPanel1.ColorScheme = null;
            this.shadedPanel1.Controls.Add(this.shadedPanel2);
            this.shadedPanel1.Location = new System.Drawing.Point(872, 3);
            this.shadedPanel1.Name = "shadedPanel1";
            this.shadedPanel1.Size = new System.Drawing.Size(195, 450);
            this.shadedPanel1.TabIndex = 4;
            this.shadedPanel1.Click += new System.EventHandler(this.Buttons_Unfocus);
            // 
            // shadedPanel2
            // 
            this.shadedPanel2.ColorScheme = null;
            this.shadedPanel2.Controls.Add(this.flowLayoutPanel1);
            this.shadedPanel2.Controls.Add(this.action_label);
            this.shadedPanel2.Controls.Add(this.imageModernButton1);
            this.shadedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.shadedPanel2.Location = new System.Drawing.Point(0, 190);
            this.shadedPanel2.Name = "shadedPanel2";
            this.shadedPanel2.Size = new System.Drawing.Size(195, 260);
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
            // action_label
            // 
            this.action_label.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.action_label.Location = new System.Drawing.Point(102, 13);
            this.action_label.Name = "action_label";
            this.action_label.Size = new System.Drawing.Size(142, 36);
            this.action_label.TabIndex = 3;
            this.action_label.Text = "Action Text";
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
            // painel_developer
            // 
            this.painel_developer.Controls.Add(this.imageModernButton5);
            this.painel_developer.Controls.Add(this.imageModernButton4);
            this.painel_developer.Controls.Add(this.imageModernButton3);
            this.painel_developer.Controls.Add(this.imageModernButton2);
            this.painel_developer.Location = new System.Drawing.Point(4, 89);
            this.painel_developer.Name = "painel_developer";
            this.painel_developer.Size = new System.Drawing.Size(570, 41);
            this.painel_developer.TabIndex = 5;
            this.painel_developer.Visible = false;
            this.painel_developer.Paint += new System.Windows.Forms.PaintEventHandler(this.Painel_developer_Paint);
            // 
            // imageModernButton5
            // 
            this.imageModernButton5.CustomColorScheme = false;
            this.imageModernButton5.Image = null;
            this.imageModernButton5.Location = new System.Drawing.Point(117, 5);
            this.imageModernButton5.Name = "imageModernButton5";
            this.imageModernButton5.NormalImage = null;
            this.imageModernButton5.Origin = null;
            this.imageModernButton5.Size = new System.Drawing.Size(112, 33);
            this.imageModernButton5.TabIndex = 2;
            this.imageModernButton5.Text = "Recarregar botões externos";
            this.imageModernButton5.UseVisualStyleBackColor = true;
            this.imageModernButton5.Click += new System.EventHandler(this.ImageModernButton6_Click);
            // 
            // imageModernButton4
            // 
            this.imageModernButton4.CustomColorScheme = false;
            this.imageModernButton4.Image = null;
            this.imageModernButton4.Location = new System.Drawing.Point(359, 5);
            this.imageModernButton4.Name = "imageModernButton4";
            this.imageModernButton4.NormalImage = null;
            this.imageModernButton4.Origin = null;
            this.imageModernButton4.Size = new System.Drawing.Size(118, 33);
            this.imageModernButton4.TabIndex = 1;
            this.imageModernButton4.Text = "Abrir console";
            this.imageModernButton4.UseVisualStyleBackColor = true;
            this.imageModernButton4.Click += new System.EventHandler(this.ImageModernButton4_Click);
            // 
            // imageModernButton3
            // 
            this.imageModernButton3.CustomColorScheme = false;
            this.imageModernButton3.Image = null;
            this.imageModernButton3.Location = new System.Drawing.Point(235, 5);
            this.imageModernButton3.Name = "imageModernButton3";
            this.imageModernButton3.NormalImage = null;
            this.imageModernButton3.Origin = null;
            this.imageModernButton3.Size = new System.Drawing.Size(118, 33);
            this.imageModernButton3.TabIndex = 1;
            this.imageModernButton3.Text = "Recarregar botões";
            this.imageModernButton3.UseVisualStyleBackColor = true;
            this.imageModernButton3.Click += new System.EventHandler(this.ImageModernButton3_Click);
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.Location = new System.Drawing.Point(4, 5);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(107, 33);
            this.imageModernButton2.TabIndex = 0;
            this.imageModernButton2.Text = "Recarregar tudo";
            this.imageModernButton2.UseVisualStyleBackColor = true;
            this.imageModernButton2.Click += new System.EventHandler(this.ImageModernButton2_Click);
            // 
            // shadedPanel4
            // 
            this.shadedPanel4.ColorScheme = null;
            this.shadedPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shadedPanel4.Location = new System.Drawing.Point(3, 3);
            this.shadedPanel4.Name = "shadedPanel4";
            this.shadedPanel4.Size = new System.Drawing.Size(132, 450);
            this.shadedPanel4.TabIndex = 6;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.8972F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.31776F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.69159F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.shadedPanel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.shadedPanel1, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 133);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1070, 456);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1093, 590);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.painel_developer);
            this.Controls.Add(this.appBar1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(915, 535);
            this.Name = "MainForm";
            this.Tag = "";
            this.Text = "ButtonDeck";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.Buttons_Unfocus);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.shadedPanel1.ResumeLayout(false);
            this.shadedPanel2.ResumeLayout(false);
            this.painel_developer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NickAc.ModernUIDoneRight.Controls.AppBar appBar1;
        private NickAc.ModernUIDoneRight.Controls.ModernShadowPanel panel1;
        private System.Windows.Forms.Label warning_label;
        private ShadedPanel shadedPanel2;
        private ImageModernButton imageModernButton1;
        private System.Windows.Forms.Label action_label;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ShadedPanel painel_developer;
        private ImageModernButton imageModernButton3;
        private ImageModernButton imageModernButton2;
        private ImageModernButton imageModernButton4;
        private ImageModernButton imageModernButton5;
        private ShadedPanel shadedPanel4;
        private NotifyIcon notifyIcon1;
        public TableLayoutPanel tableLayoutPanel1;
        public ShadedPanel shadedPanel1;

        internal ShadedPanel ShadedPanel1 { get => shadedPanel1; set => shadedPanel1 = value; }
    }
}