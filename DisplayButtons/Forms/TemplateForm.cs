using DisplayButtons.Controls;

using DisplayButtons.Properties;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using NickAc.ModernUIDoneRight.Forms;
using NickAc.ModernUIDoneRight.Objects;
using Misc;
using BackendAPI.Utils;
using static BackendAPI.Utils.AppSettings;

namespace DisplayButtons.Forms
{


    public class TemplateForm : ModernForm
    {
        public bool MinimizeToTrayRightMinimize { get; set; }

        private ApplicationColorScheme _applicationColorScheme;

        private ApplicationColorScheme ApplicationColorScheme {
            get { return _applicationColorScheme; }
            set {
                _applicationColorScheme = value;
                ColorScheme = new ColorScheme(value.PrimaryColor, value.SecondaryColor);
                BackColor = value.BackgroundColor;
                Refresh();
            }
        }
        public TemplateForm()
        {
            if (ApplicationSettingsManager.Settings != null) {
                LoadTheme(ApplicationSettingsManager.Settings.Theme);
                ModifyColorScheme(Controls.OfType<Control>());
            }
            ColorSchemeCentral.ThemeChanged += (s, e) => {
                LoadTheme(ApplicationSettingsManager.Settings.Theme);
                ModifyColorScheme(Controls.OfType<Control>());
            };
            Icon = Resources.button_deck;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (ApplicationSettingsManager.Settings != null) {
                LoadTheme(ApplicationSettingsManager.Settings.Theme);
            }
            base.OnLoad(e);
            ModifyColorScheme(Controls.OfType<Control>());

        }
      
        private static Color GetReadableForeColor(Color c)
        {
            return (((c.R + c.B + c.G) / 3) > 128) ? Color.Black : Color.White;
        }

        public void ModifyColorScheme(IEnumerable<Control> cccc)
        {
         
            cccc.All(c => {
              
                if (c.Tag != null && c.Tag.ToString() == "noColor")
                    return true;
                ModifyColorScheme(c.Controls.OfType<Control>());
                try
                {
                    dynamic cc = c;
                    
                        if (c is TextBox txtBx)
                        {
                            txtBx.BorderStyle = BorderStyle.FixedSingle;
                            cc.BackColor = _applicationColorScheme.BackgroundColor;
                            c.ForeColor = GetReadableForeColor(_applicationColorScheme.BackgroundColor);
                        }
cc.ForeColor = _applicationColorScheme.ForeColorShaded;

                    
              if(c.GetType().GetProperties().Any(x => x.Name == "ColorScheme"))
                    {
   cc.ColorScheme = ColorScheme;

                    }
                 

            
                       
                    
                    return true;
                }
                catch (Exception ee)
                {
                    Debug.WriteLine(ee);
                    return true;
                }
            });/*
    cccc.OfType<ColorSchemePreviewControl>().All(c => {
    c.AppTheme = ColorSchemeCentral.FromAppTheme(c.UnderlyingAppTheme);
    return true; });*/
        }

     
        private void LoadTheme(AppTheme theme)
        {
            ApplicationColorScheme = ColorSchemeCentral.FromAppTheme(theme);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TemplateForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.ColorScheme.isToIgnoreForegroundColor = false;
            this.ColorScheme.MouseDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(64)))), ((int)(((byte)(101)))));
            this.ColorScheme.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.ColorScheme.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.ColorScheme.SecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "TemplateForm";
            this.Load += new System.EventHandler(this.TemplateForm_Load);
            this.ResumeLayout(false);

        }

        private void TemplateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
