using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Backend.Utils
{
   
        public static class KeyEventExts
        {
         
            public static System.Windows.Forms.Keys ToWinforms(this System.Windows.Input.ModifierKeys modifier)
            {
                var retVal = System.Windows.Forms.Keys.None;
                if (modifier.HasFlag(System.Windows.Input.ModifierKeys.Alt))
                {
                    retVal |= System.Windows.Forms.Keys.Alt;
                }
                if (modifier.HasFlag(System.Windows.Input.ModifierKeys.Control))
                {
                    retVal |= System.Windows.Forms.Keys.Control;
                }
                if (modifier.HasFlag(System.Windows.Input.ModifierKeys.None))
                {
                    // Pointless I know
                    retVal |= System.Windows.Forms.Keys.None;
                }
                if (modifier.HasFlag(System.Windows.Input.ModifierKeys.Shift))
                {
                    retVal |= System.Windows.Forms.Keys.Shift;
                }
                if (modifier.HasFlag(System.Windows.Input.ModifierKeys.Windows))
                {
                    // Not supported lel
                }
                return retVal;
            }
        
    }
}
