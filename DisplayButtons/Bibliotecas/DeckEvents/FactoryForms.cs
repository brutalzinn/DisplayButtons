using DisplayButtons.Forms.EventSystem.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    public class FactoryForms
    {

       
        public void SaveButton( params object[] values)
        {

         





        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public class ListView
        {
            public string Text { get; set; }
            public AbstractTrigger Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
