using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckText
{
    public class TextLabel
    {

        private string text;
        private Brush brush;
        private Font font;
        private float position;
        private int size;




        public TextLabel(IDeckItem item)
        {
            FontFamily fontFamily = new FontFamily("Arial");



            this.position = item.Deckposition;
            this.Size = item.Decksize;

            this.text = item.Deckname;
            this.brush = Brushes.White;
            this.font = new Font(
   fontFamily,
   16,
   FontStyle.Regular,
   GraphicsUnit.Pixel);
          
        }

        public string Text { get => text; set => text = value; }
        public Brush Brush { get => brush; set => brush = value; }
        public Font Font { get => font; set => font = value; }
        public float Position { get => position; set => position = value; }
        public int Size { get => size; set => size = value; }
    }
}
