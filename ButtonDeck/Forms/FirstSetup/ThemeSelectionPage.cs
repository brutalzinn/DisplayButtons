using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ButtonDeck.Misc;
using ButtonDeck.Controls;
using ButtonDeck.Backend.Utils;
using NickAc.ModernUIDoneRight.Utils;

namespace ButtonDeck.Forms.FirstSetup
{
    public partial class ThemeSelectionPage : PageTemplate
    {
        public ThemeSelectionPage()
        {
            InitializeComponent();
            //Set the default theme



      colorSchemePreviewControl1.Tag = true;

            colorSchemePreviewControl1.AppTheme = ColorSchemeCentral.DarkSide;
   colorSchemePreviewControl1.UnderlyingAppTheme = AppSettings.AppTheme.DarkSide;

            colorSchemePreviewControl2.Tag = true;
            colorSchemePreviewControl2.AppTheme = ColorSchemeCentral.KindaGreen;
            colorSchemePreviewControl2.UnderlyingAppTheme = AppSettings.AppTheme.KindaGreen;

            colorSchemePreviewControl3.Tag = true;
            colorSchemePreviewControl3.AppTheme = ColorSchemeCentral.Neptune;
            colorSchemePreviewControl3.UnderlyingAppTheme = AppSettings.AppTheme.Neptune;

            colorSchemePreviewControl4.Tag = true;
            colorSchemePreviewControl4.AppTheme = ColorSchemeCentral.PinkNanda;
            colorSchemePreviewControl4.UnderlyingAppTheme = AppSettings.AppTheme.PinkNanda;


        }
        


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var c in Controls.OfType<ColorSchemePreviewControl>()) {
                if (c.Tag != null) {
                    using (var border = new SolidBrush(ControlPaint.DarkDark(CurrentTheme.SecondaryColor))) {
                        var rect = Rectangle.Inflate(c.Bounds, 2, 2);
                        e.Graphics.FillRectangle(border, rect);
                    }
                }
            }
        }

        private void ColorSchemePreviewControl2_Click(object sender, EventArgs e)
        {
            if (sender is ColorSchemePreviewControl ctrl) {
                Controls.OfType<ColorSchemePreviewControl>().All((c) => {
                    c.Tag = null;
                    return true;
                });
                ctrl.Tag = true;
                Refresh();
          
                ApplicationSettingsManager.Settings.Theme = ctrl.UnderlyingAppTheme;
                ColorSchemeCentral.OnThemeChanged(this);
            }
        }

        private void ThemeSelectionPage_Load(object sender, EventArgs e)
        {

            Texts.initilizeLang();
            this.Refresh();
            label4.Text = Texts.rm.GetString("APPLICATIONFIRSTSETUPAGE1_INTROPAGE_LABEL6", Texts.cultereinfo);

        }
    }
}
