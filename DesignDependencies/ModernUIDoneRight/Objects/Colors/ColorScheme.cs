using NickAc.ModernUIDoneRight.Utils;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static NickAc.ModernUIDoneRight.Utils.GraphicUtils;
namespace NickAc.ModernUIDoneRight.Objects
{

    [TypeConverter(typeof(ColorSchemeConverter))]
    public class ColorScheme
    {
        #region Constructor
        public ColorScheme(Color primaryColor, Color secondaryColor, bool ignoreDarkness = false)
        {
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            MouseDownColor = DarkenColor(primaryColor, 0.2f);
            MouseHoverColor = LightenColor(secondaryColor, 0.2F);
            isToIgnoreForegroundColor = ignoreDarkness;
            if (primaryColor.IsDarker())
            {
                MouseDownColor = LightenColor(MouseDownColor, 0.2f);
                MouseHoverColor = LightenColor(MouseDownColor, 0.25f);
            }
          //  Debug.WriteLine(ignoreDarkness);


        }
        #endregion

        #region Properties
        public Color PrimaryColor { get; set; }
        public bool isToIgnoreForegroundColor { get; set; }
        public Color SecondaryColor { get; set; }
        public Color MouseDownColor { get; set; }
        public Color MouseHoverColor { get; set; }
        public Color ForegroundColor
        {

            get
            {

                if (isToIgnoreForegroundColor)
                {
                    return ForegroundColorForBackground(PrimaryColor);
                }
                else
                {
                   return Color.White;
                }
            }
           
        }
            // => ForegroundColorForBackground(PrimaryColor)
        #endregion

        #region Static Methods
        public static Color DarkenColor(Color original, float value = 0.05f)
        {
            return ControlPaint.Dark(original, value);
        }

        public static Color LightenColor(Color original, float value = 0.05f)
        {
            return ControlPaint.Light(original, value);
        }
        
        public static ColorScheme CreateSimpleColorScheme(Color original)
        {
            return new ColorScheme(original, DarkenColor(original));
        }
        #endregion
    }
}
