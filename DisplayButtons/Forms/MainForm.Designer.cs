using DisplayButtons.Controls;
using System;
using System.Windows.Forms;

namespace DisplayButtons.Forms
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
            this.shadedPanel1 = new DisplayButtons.Forms.ShadedPanel();
            this.shadedPanel2 = new DisplayButtons.Forms.ShadedPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.camada2 = new System.Windows.Forms.RadioButton();
            this.camada1 = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.action_label = new System.Windows.Forms.Label();
            this.ActionImagePlaceHolder = new DisplayButtons.Controls.ImageModernButton();
            this.painel_developer = new DisplayButtons.Forms.ShadedPanel();
            this.shadedPanel3 = new DisplayButtons.Forms.ShadedPanel();
            this.imageModernButton4 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton2 = new DisplayButtons.Controls.ImageModernButton();
            this.shadedPanel4 = new DisplayButtons.Forms.ShadedPanel();
            this.panel_buttons = new System.Windows.Forms.Panel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.link = new System.Windows.Forms.ToolStripStatusLabel();
            this.info = new System.Windows.Forms.ToolStripStatusLabel();
            this.warning_label = new System.Windows.Forms.Label();
            this.imageModernButton6 = new DisplayButtons.Controls.ImageModernButton();
            this.perfilselector = new System.Windows.Forms.ComboBox();
            this.imageModernButton7 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton8 = new DisplayButtons.Controls.ImageModernButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.perfil_info = new System.Windows.Forms.Label();
            this.deckoptions_button = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton1 = new DisplayButtons.Controls.ImageModernButton();
            this.imageModernButton3 = new DisplayButtons.Controls.ImageModernButton();
            this.shadedPanel1.SuspendLayout();
            this.shadedPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.painel_developer.SuspendLayout();
            this.panel_buttons.SuspendLayout();
            this.statusStrip2.SuspendLayout();
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
            this.appBar1.Size = new System.Drawing.Size(1251, 50);
            this.appBar1.TabIndex = 0;
            this.appBar1.Text = "DisplayButtons";
            this.appBar1.TextFont = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.appBar1.ToolTip = null;
            this.appBar1.Click += new System.EventHandler(this.appBar1_Click);
            // 
            // shadedPanel1
            // 
            this.shadedPanel1.Controls.Add(this.shadedPanel2);
            this.shadedPanel1.Location = new System.Drawing.Point(950, 152);
            this.shadedPanel1.Name = "shadedPanel1";
            this.shadedPanel1.Size = new System.Drawing.Size(302, 486);
            this.shadedPanel1.TabIndex = 4;
            this.shadedPanel1.Click += new System.EventHandler(this.Buttons_Unfocus);
            // 
            // shadedPanel2
            // 
            this.shadedPanel2.ColorScheme = null;
            this.shadedPanel2.Controls.Add(this.groupBox1);
            this.shadedPanel2.Controls.Add(this.flowLayoutPanel1);
            this.shadedPanel2.Controls.Add(this.action_label);
            this.shadedPanel2.Controls.Add(this.ActionImagePlaceHolder);
            this.shadedPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.shadedPanel2.Location = new System.Drawing.Point(0, 224);
            this.shadedPanel2.Name = "shadedPanel2";
            this.shadedPanel2.Size = new System.Drawing.Size(302, 262);
            this.shadedPanel2.TabIndex = 0;
            this.shadedPanel2.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.camada2);
            this.groupBox1.Controls.Add(this.camada1);
            this.groupBox1.Location = new System.Drawing.Point(101, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 36);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camada";
            // 
            // camada2
            // 
            this.camada2.AutoSize = true;
            this.camada2.Location = new System.Drawing.Point(27, 22);
            this.camada2.Name = "camada2";
            this.camada2.Size = new System.Drawing.Size(14, 13);
            this.camada2.TabIndex = 1;
            this.camada2.UseVisualStyleBackColor = true;
            this.camada2.Visible = false;
            this.camada2.Click += new System.EventHandler(this.camada2_Click);
            // 
            // camada1
            // 
            this.camada1.AutoSize = true;
            this.camada1.Checked = true;
            this.camada1.Location = new System.Drawing.Point(6, 22);
            this.camada1.Name = "camada1";
            this.camada1.Size = new System.Drawing.Size(14, 13);
            this.camada1.TabIndex = 0;
            this.camada1.TabStop = true;
            this.camada1.UseVisualStyleBackColor = true;
            this.camada1.Click += new System.EventHandler(this.camada1_Click);
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
            this.action_label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.action_label.Location = new System.Drawing.Point(101, 13);
            this.action_label.Name = "action_label";
            this.action_label.Size = new System.Drawing.Size(142, 36);
            this.action_label.TabIndex = 3;
            this.action_label.Text = "Action Text";
            // 
            // ActionImagePlaceHolder
            // 
            this.ActionImagePlaceHolder.Camada = 1;
            this.ActionImagePlaceHolder.CustomColorScheme = false;
            this.ActionImagePlaceHolder.Image = null;
            this.ActionImagePlaceHolder.ImageLayerTwo = null;
            this.ActionImagePlaceHolder.Location = new System.Drawing.Point(15, 13);
            this.ActionImagePlaceHolder.Name = "ActionImagePlaceHolder";
            this.ActionImagePlaceHolder.NormalImage = null;
            this.ActionImagePlaceHolder.NormalLayerTwo = null;
            this.ActionImagePlaceHolder.Origin = null;
            this.ActionImagePlaceHolder.Size = new System.Drawing.Size(80, 80);
            this.ActionImagePlaceHolder.TabIndex = 2;
            this.ActionImagePlaceHolder.TextButton = null;
            this.ActionImagePlaceHolder.UseVisualStyleBackColor = true;
            this.ActionImagePlaceHolder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ImageModernButton1_MouseClick);
            // 
            // painel_developer
            // 
            this.painel_developer.Controls.Add(this.shadedPanel3);
            this.painel_developer.Controls.Add(this.imageModernButton4);
            this.painel_developer.Controls.Add(this.imageModernButton2);
            this.painel_developer.Location = new System.Drawing.Point(545, 89);
            this.painel_developer.Name = "painel_developer";
            this.painel_developer.Size = new System.Drawing.Size(283, 47);
            this.painel_developer.TabIndex = 5;
            this.painel_developer.Visible = false;
            this.painel_developer.Paint += new System.Windows.Forms.PaintEventHandler(this.Painel_developer_Paint);
            // 
            // shadedPanel3
            // 
            this.shadedPanel3.ColorScheme = null;
            this.shadedPanel3.Location = new System.Drawing.Point(3, 49);
            this.shadedPanel3.Name = "shadedPanel3";
            this.shadedPanel3.Size = new System.Drawing.Size(189, 456);
            this.shadedPanel3.TabIndex = 3;
            // 
            // imageModernButton4
            // 
            this.imageModernButton4.Camada = 0;
            this.imageModernButton4.CustomColorScheme = false;
            this.imageModernButton4.Image = null;
            this.imageModernButton4.ImageLayerTwo = null;
            this.imageModernButton4.Location = new System.Drawing.Point(139, 7);
            this.imageModernButton4.Name = "imageModernButton4";
            this.imageModernButton4.NormalImage = null;
            this.imageModernButton4.NormalLayerTwo = null;
            this.imageModernButton4.Origin = null;
            this.imageModernButton4.Size = new System.Drawing.Size(124, 34);
            this.imageModernButton4.TabIndex = 1;
            this.imageModernButton4.Text = "Abrir console";
            this.imageModernButton4.TextButton = null;
            this.imageModernButton4.UseVisualStyleBackColor = true;
            this.imageModernButton4.Click += new System.EventHandler(this.ImageModernButton4_Click);
            // 
            // imageModernButton2
            // 
            this.imageModernButton2.Camada = 0;
            this.imageModernButton2.CustomColorScheme = false;
            this.imageModernButton2.Image = null;
            this.imageModernButton2.ImageLayerTwo = null;
            this.imageModernButton2.Location = new System.Drawing.Point(14, 8);
            this.imageModernButton2.Name = "imageModernButton2";
            this.imageModernButton2.NormalImage = null;
            this.imageModernButton2.NormalLayerTwo = null;
            this.imageModernButton2.Origin = null;
            this.imageModernButton2.Size = new System.Drawing.Size(107, 33);
            this.imageModernButton2.TabIndex = 0;
            this.imageModernButton2.Text = "Recarregar tudo";
            this.imageModernButton2.TextButton = null;
            this.imageModernButton2.UseVisualStyleBackColor = true;
            this.imageModernButton2.Click += new System.EventHandler(this.ImageModernButton2_Click);
            // 
            // shadedPanel4
            // 
            this.shadedPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shadedPanel4.Location = new System.Drawing.Point(1, 152);
            this.shadedPanel4.Name = "shadedPanel4";
            this.shadedPanel4.Size = new System.Drawing.Size(136, 486);
            this.shadedPanel4.TabIndex = 6;
            // 
            // panel_buttons
            // 
            this.panel_buttons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_buttons.AutoScroll = true;
            this.panel_buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_buttons.Controls.Add(this.statusStrip2);
            this.panel_buttons.Controls.Add(this.warning_label);
            this.panel_buttons.Location = new System.Drawing.Point(143, 149);
            this.panel_buttons.Name = "panel_buttons";
            this.panel_buttons.Size = new System.Drawing.Size(801, 489);
            this.panel_buttons.TabIndex = 7;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.link,
            this.info});
            this.statusStrip2.Location = new System.Drawing.Point(0, 467);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip2.Size = new System.Drawing.Size(801, 22);
            this.statusStrip2.TabIndex = 1;
            this.statusStrip2.Text = "statusStrip2";
            this.statusStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip2_ItemClicked);
            // 
            // link
            // 
            this.link.Name = "link";
            this.link.Size = new System.Drawing.Size(112, 17);
            this.link.Text = "displaybuttons.com";
            // 
            // info
            // 
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(28, 17);
            this.info.Text = "Info";
            // 
            // warning_label
            // 
            this.warning_label.AutoSize = true;
            this.warning_label.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.warning_label.Location = new System.Drawing.Point(20, 240);
            this.warning_label.Name = "warning_label";
            this.warning_label.Size = new System.Drawing.Size(765, 40);
            this.warning_label.TabIndex = 0;
            this.warning_label.Text = "CREATE A PERFIL TO USE DISPLAYBUTTONS";
            // 
            // imageModernButton6
            // 
            this.imageModernButton6.Camada = 0;
            this.imageModernButton6.CustomColorScheme = false;
            this.imageModernButton6.Image = null;
            this.imageModernButton6.ImageLayerTwo = null;
            this.imageModernButton6.Location = new System.Drawing.Point(250, 93);
            this.imageModernButton6.Name = "imageModernButton6";
            this.imageModernButton6.NormalImage = null;
            this.imageModernButton6.NormalLayerTwo = null;
            this.imageModernButton6.Origin = null;
            this.imageModernButton6.Size = new System.Drawing.Size(123, 37);
            this.imageModernButton6.TabIndex = 8;
            this.imageModernButton6.Text = "EventSystem";
            this.imageModernButton6.TextButton = null;
            this.imageModernButton6.UseVisualStyleBackColor = true;
            this.imageModernButton6.Click += new System.EventHandler(this.imageModernButton6_Click_1);
            // 
            // perfilselector
            // 
            this.perfilselector.FormattingEnabled = true;
            this.perfilselector.Location = new System.Drawing.Point(61, 104);
            this.perfilselector.Name = "perfilselector";
            this.perfilselector.Size = new System.Drawing.Size(86, 23);
            this.perfilselector.TabIndex = 9;
            this.perfilselector.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // imageModernButton7
            // 
            this.imageModernButton7.Camada = 0;
            this.imageModernButton7.CustomColorScheme = false;
            this.imageModernButton7.Image = null;
            this.imageModernButton7.ImageLayerTwo = null;
            this.imageModernButton7.Location = new System.Drawing.Point(153, 99);
            this.imageModernButton7.Name = "imageModernButton7";
            this.imageModernButton7.NormalImage = null;
            this.imageModernButton7.NormalLayerTwo = null;
            this.imageModernButton7.Origin = null;
            this.imageModernButton7.Size = new System.Drawing.Size(27, 31);
            this.imageModernButton7.TabIndex = 10;
            this.imageModernButton7.Text = "+";
            this.imageModernButton7.TextButton = null;
            this.imageModernButton7.UseVisualStyleBackColor = true;
            this.imageModernButton7.Click += new System.EventHandler(this.imageModernButton7_Click);
            // 
            // imageModernButton8
            // 
            this.imageModernButton8.Camada = 0;
            this.imageModernButton8.CustomColorScheme = false;
            this.imageModernButton8.Image = null;
            this.imageModernButton8.ImageLayerTwo = null;
            this.imageModernButton8.Location = new System.Drawing.Point(186, 99);
            this.imageModernButton8.Name = "imageModernButton8";
            this.imageModernButton8.NormalImage = null;
            this.imageModernButton8.NormalLayerTwo = null;
            this.imageModernButton8.Origin = null;
            this.imageModernButton8.Size = new System.Drawing.Size(28, 31);
            this.imageModernButton8.TabIndex = 10;
            this.imageModernButton8.Text = "-";
            this.imageModernButton8.TextButton = null;
            this.imageModernButton8.UseVisualStyleBackColor = true;
            this.imageModernButton8.Click += new System.EventHandler(this.imageModernButton8_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 20);
            this.toolStripDropDownButton1.Text = "Informações";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem1.Text = "Sobre";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem2.Text = "Bibliotecas usadas";
            // 
            // perfil_info
            // 
            this.perfil_info.AutoSize = true;
            this.perfil_info.Location = new System.Drawing.Point(13, 108);
            this.perfil_info.Name = "perfil_info";
            this.perfil_info.Size = new System.Drawing.Size(38, 15);
            this.perfil_info.TabIndex = 11;
            this.perfil_info.Text = "label1";
            // 
            // deckoptions_button
            // 
            this.deckoptions_button.Camada = 0;
            this.deckoptions_button.CustomColorScheme = false;
            this.deckoptions_button.Image = null;
            this.deckoptions_button.ImageLayerTwo = null;
            this.deckoptions_button.Location = new System.Drawing.Point(390, 93);
            this.deckoptions_button.Name = "deckoptions_button";
            this.deckoptions_button.NormalImage = null;
            this.deckoptions_button.NormalLayerTwo = null;
            this.deckoptions_button.Origin = null;
            this.deckoptions_button.Size = new System.Drawing.Size(122, 37);
            this.deckoptions_button.TabIndex = 12;
            this.deckoptions_button.Text = "Deck options";
            this.deckoptions_button.TextButton = null;
            this.deckoptions_button.UseVisualStyleBackColor = true;
            this.deckoptions_button.Click += new System.EventHandler(this.imageModernButton9_Click);
            // 
            // imageModernButton1
            // 
            this.imageModernButton1.Camada = 0;
            this.imageModernButton1.CustomColorScheme = false;
            this.imageModernButton1.Image = null;
            this.imageModernButton1.ImageLayerTwo = null;
            this.imageModernButton1.Location = new System.Drawing.Point(850, 89);
            this.imageModernButton1.Name = "imageModernButton1";
            this.imageModernButton1.NormalImage = null;
            this.imageModernButton1.NormalLayerTwo = null;
            this.imageModernButton1.Origin = null;
            this.imageModernButton1.Size = new System.Drawing.Size(94, 43);
            this.imageModernButton1.TabIndex = 13;
            this.imageModernButton1.Text = "criar";
            this.imageModernButton1.TextButton = null;
            this.imageModernButton1.UseVisualStyleBackColor = true;
            this.imageModernButton1.Click += new System.EventHandler(this.imageModernButton1_Click);
            // 
            // imageModernButton3
            // 
            this.imageModernButton3.Camada = 0;
            this.imageModernButton3.CustomColorScheme = false;
            this.imageModernButton3.Image = null;
            this.imageModernButton3.ImageLayerTwo = null;
            this.imageModernButton3.Location = new System.Drawing.Point(965, 92);
            this.imageModernButton3.Name = "imageModernButton3";
            this.imageModernButton3.NormalImage = null;
            this.imageModernButton3.NormalLayerTwo = null;
            this.imageModernButton3.Origin = null;
            this.imageModernButton3.Size = new System.Drawing.Size(94, 43);
            this.imageModernButton3.TabIndex = 13;
            this.imageModernButton3.Text = "salvar";
            this.imageModernButton3.TextButton = null;
            this.imageModernButton3.UseVisualStyleBackColor = true;
            this.imageModernButton3.Click += new System.EventHandler(this.imageModernButton3_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1253, 651);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Controls.Add(this.imageModernButton3);
            this.Controls.Add(this.imageModernButton1);
            this.Controls.Add(this.deckoptions_button);
            this.Controls.Add(this.perfil_info);
            this.Controls.Add(this.imageModernButton8);
            this.Controls.Add(this.imageModernButton7);
            this.Controls.Add(this.perfilselector);
            this.Controls.Add(this.imageModernButton6);
            this.Controls.Add(this.panel_buttons);
            this.Controls.Add(this.shadedPanel1);
            this.Controls.Add(this.shadedPanel4);
            this.Controls.Add(this.painel_developer);
            this.Controls.Add(this.appBar1);
            this.MinimumSize = new System.Drawing.Size(915, 535);
            this.Name = "MainForm";
            this.ShadowType = NickAc.ModernUIDoneRight.Objects.ShadowType.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "DisplayButtons";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.Buttons_Unfocus);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.shadedPanel1.ResumeLayout(false);
            this.shadedPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.painel_developer.ResumeLayout(false);
            this.panel_buttons.ResumeLayout(false);
            this.panel_buttons.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NickAc.ModernUIDoneRight.Controls.AppBar appBar1;
        private ShadedPanel shadedPanel2;
        private ImageModernButton ActionImagePlaceHolder;
        private System.Windows.Forms.Label action_label;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ShadedPanel painel_developer;
        private ImageModernButton imageModernButton2;
        private ImageModernButton imageModernButton4;
        private ShadedPanel shadedPanel3;
        private ShadedPanel shadedPanel4;
        private ImageModernButton imageModernButton6;
        private ImageModernButton imageModernButton7;
        private ImageModernButton imageModernButton8;
        public Panel panel_buttons;
        public ShadedPanel shadedPanel1;
        public Label warning_label;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private StatusStrip statusStrip2;
        private ToolStripStatusLabel link;
  
        private ToolStripStatusLabel info;
        private Label perfil_info;
        public ComboBox perfilselector;
        private ImageModernButton deckoptions_button;
        private RadioButton camada2;
        private RadioButton camada1;
        public GroupBox groupBox1;
        private ImageModernButton imageModernButton1;
        private ImageModernButton imageModernButton3;

        internal ShadedPanel ShadedPanel1 { get => shadedPanel1; set => shadedPanel1 = value; }
    }
}