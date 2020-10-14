using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotecas.DeckText
{
   public class TextHelper
    {
        public static double PercentOf(double value, double total)
        {

            return (100.0 / total) * value;

        }
    }
}
