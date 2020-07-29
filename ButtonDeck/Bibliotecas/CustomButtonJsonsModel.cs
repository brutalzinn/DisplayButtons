using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonDeck.Bibliotecas
{
    class CustomButtonJsonsModel
    {
        public class Labels
        {
            private int id;
            private string font;
            private int size;
            private string text;
            private int position;
            private string color;
            private DeckImage image;
            public Labels(int id, string font, int size, int position, string text, string color, DeckImage img)
            {
                if (String.IsNullOrEmpty(text))
                {

                    text = "-";
                }
                this.id = id;
                this.font = font;
                this.text = text;
                this.size = size;
                this.position = position;
                this.color = color;
                this.image = img;

            }
            public DeckImage Image
            {

                get { return image; }
                set { image = value; }

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
            public int Id
            {

                get { return id; }
                set { id = value; }
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



        }
    }
}
