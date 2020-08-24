using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Backend.Objects
{
    [Serializable]
    public class MatrizObject
    {

        private int lin;
        private int column;
        private int calc;

        public MatrizObject() { }
        public MatrizObject(int lin, int column)
        {
            this.Lin = lin;
            this.Column = column;
            this.calc = lin * column;
        }

        public int Lin { get => lin; set => lin = value; }
        public int Column { get => column; set => column = value; }
        public int Calc { get => calc; set => calc = value; }

    }
}
