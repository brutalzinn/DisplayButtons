using DisplayButtons.Backend.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayButtons.Bibliotecas
{
 
        public class Labels
        {
            private int id;
        private IDeckItem item;
        private DeckImage image;

        public Labels(int id, IDeckItem item, DeckImage image)
        {
            this.id = id;
            this.item = item;
            this.image = image;
        }

        public int Id { get => id; set => id = value; }
        public IDeckItem Item { get => item; set => item = value; }
        public DeckImage Image { get => image; set => image = value; }




        // Should also override == and != operators.
    }
        public class Json
        {
            private int slot;
            private int arraylenght;
            private byte[] internalbtpm;
            private string font;
            private int size;
            private string text;
            private int position;
            private string color;
        private float _shadowradius = 2.6f;
        private float stroke_dxtext = 1.5f;
        private float dytextfloat = 1.3f;
        private string stroke_color = "#FFFFFF";
        private bool _isforstroke = false;
        private bool ishinttext;
        private bool isboldtext;
        private bool isnormaltext;
        private bool isitalictext;


        public Json()

            {




            }
            public int ArrayLenght
            {

                get { return arraylenght; }
                set { arraylenght = value; }

            }
            public byte[] InternalBtpm
            {

                get { return internalbtpm; }
                set { internalbtpm = value; }

            }
            public string Color
            {

                get { return color; }
                set { color = value; }

            }
            public string Text
            {
                get { return text; }
                set { text = value; }
            }
            public int Slot
            {

                get { return slot; }
                set { slot = value; }
            }

            public string Font
            {

                get { return font; }
                set { font = value; }


            }

            public int Size
            {
                get { return size; }
                set { size = value; }
            }
            public int Position
            {
                get { return position; }
                set { position = value; }
            }

        public float Stroke_radius { get => _shadowradius; set => _shadowradius = value; }
        public float Stroke_dx { get => stroke_dxtext; set => stroke_dxtext = value; }
        public float Stroke_dy { get => dytextfloat; set => dytextfloat = value; }
        public string Stroke_color { get => stroke_color; set => stroke_color = value; }
        public bool IsStroke { get => _isforstroke; set => _isforstroke = value; }
        public bool Ishinttext { get => ishinttext; set => ishinttext = value; }
        public bool Isboldtext { get => isboldtext; set => isboldtext = value; }
        public bool Isnormaltext { get => isnormaltext; set => isnormaltext = value; }
        public bool Isitalictext { get => isitalictext; set => isitalictext = value; }
    }
    
}
