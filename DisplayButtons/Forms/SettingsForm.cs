﻿using DisplayButtons.Controls;

using NickAc.ModernUIDoneRight.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Globalization;
using BackendAPI;
using BackendAPI.Utils;
using Misc;

namespace DisplayButtons.Forms
{
    public partial class SettingsForm : TemplateForm
    {
        bool hasSaved = true;
        AppSettings OldSettings { get; set; }


        public SettingsForm()
        {
            OldSettings = ApplicationSettingsManager.Settings.DeepClone();
            InitializeComponent();
            modernShadowPanel1.Freeze();
            modernShadowPanel1.Paint += ModernShadowPanel1_Paint;
            PrepareColorPreviews();
            textBox1.Text = OldSettings.DeviceName;
            textBox2.Text = OldSettings.IFTTTAPIKey;
            comboBox1.Text = ApplicationSettingsManager.Settings.Language;
            textBox1.TextChanged += (s, e) => {
                if (s is TextBox txt) {
                    hasSaved = txt.Text == OldSettings.DeviceName;
                }
            };
            FormClosing += (s, e) => {
                if (!hasSaved) {
                    e.Cancel = true;
                    return;
                }
            };
        }

        private void ModernShadowPanel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var c in modernShadowPanel1.Controls.OfType<ColorSchemePreviewControl>()) {
                if (c.Tag != null) {
                    //ShadowUtils.DrawOutsetShadow(e.Graphics, Color.Black, 1, 0, 10, 0, c);
                    using (SolidBrush solidBrush = new SolidBrush(CurrentTheme.ForeColorShaded)) {
                        e.Graphics.FillRectangle(solidBrush, Rectangle.Inflate(c.Bounds, 1, 1));
                    }
                }
            }
        }

        public ApplicationColorScheme CurrentTheme {
            get {
                return ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            }
        }


        private void PrepareColorPreviews()
        {
            colorSchemePreviewControl1.AppTheme = ColorSchemeCentral.Neptune;
            colorSchemePreviewControl1.UnderlyingAppTheme = AppSettings.AppTheme.Neptune;

            colorSchemePreviewControl2.AppTheme = ColorSchemeCentral.DarkSide;
            colorSchemePreviewControl2.UnderlyingAppTheme = AppSettings.AppTheme.DarkSide;

            colorSchemePreviewControl3.AppTheme = ColorSchemeCentral.KindaGreen;
            colorSchemePreviewControl3.UnderlyingAppTheme = AppSettings.AppTheme.KindaGreen;

            colorSchemePreviewControl4.AppTheme = ColorSchemeCentral.PinkNanda;
            colorSchemePreviewControl4.UnderlyingAppTheme = AppSettings.AppTheme.PinkNanda;


            modernShadowPanel1.Controls.OfType<ColorSchemePreviewControl>().All((c) => {
                c.Tag = ApplicationSettingsManager.Settings.Theme == c.UnderlyingAppTheme ? new object() : null;
                ((ColorSchemePreviewControl)c).FixMyTheme();
                return true;
            });

        }

        private void ColorScheme_Selected(object sender, EventArgs e)
        {
            if (sender is ColorSchemePreviewControl ctrl) {
                if (ctrl.UnderlyingAppTheme == ApplicationSettingsManager.Settings.Theme) return;
                modernShadowPanel1.Controls.OfType<ColorSchemePreviewControl>().All((c) => {
                    ApplicationSettingsManager.Settings.Theme = ctrl.UnderlyingAppTheme;
                    ColorSchemeCentral.OnThemeChanged(this);
                    c.Tag = null;
                    return true;
                });
                ctrl.Tag = true;
                Refresh();
            }
        }
        private void ModernButton2_Click(object sender, EventArgs e)
        {
            hasSaved = true;
            ApplicationSettingsManager.Settings.Language = comboBox1.Text ;
            ApplicationSettingsManager.Settings.DeviceName = textBox1.Text;
            ApplicationSettingsManager.Settings.IFTTTAPIKey = textBox2.Text;
            Texts.initilizeLang();
           
            Close();
        }

        private void ModernButton1_Click(object sender, EventArgs e)
        {
            hasSaved = true;
            ApplicationSettingsManager.ReplaceAppSettings(OldSettings);
            ColorSchemeCentral.OnThemeChanged(this);
            Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Texts.initilizeLang();
            

            label1.Text = Texts.rm.GetString("DEVICENAME",Texts.cultereinfo);
            label2.Text = Texts.rm.GetString("THEME", Texts.cultereinfo);
            label4.Text = Texts.rm.GetString("LANGUAGE", Texts.cultereinfo);
            modernButton1.Text = Texts.rm.GetString("BUTTONCANCEL", Texts.cultereinfo);
            modernButton2.Text = Texts.rm.GetString("BUTTONSAVE", Texts.cultereinfo);
        }

        private void ModernShadowPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
