using DisplayButtons.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    public class PanelControl : UserControl
    {


        public virtual void SaveConfig( )
        {
           
        }
    
        public virtual UserControl getControl { get; }
        public virtual AbstractTrigger getClassImplementTrigger { get; }

        public virtual AbstractAction getClassImplementAction { get; }
    }
}
