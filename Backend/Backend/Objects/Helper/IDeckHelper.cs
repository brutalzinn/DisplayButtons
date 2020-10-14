using DisplayButtons.Bibliotecas.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Backend.Objects
{
    public interface IDeckHelper  
    {

        public static void setVariable(bool variable, IDeckItem item, DeckDevice device)
        {
            try
            {

                if (variable)
                {
                    DeckHelpers.RefreshButton(item, 2, item.GetDeckLayerTwo, device);
                }
                else
                {
                    DeckHelpers.RefreshButton(item, 1, item.GetDeckDefaultLayer, device);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
