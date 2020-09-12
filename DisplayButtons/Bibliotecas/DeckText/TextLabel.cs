using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckText
{
    public class TextLabel
    {

        private string text;
        private Brush brush;
        private Font font;
        private int position;
        private int size;
        private Color color;



        public TextLabel(IDeckItem item)
        {
            FontFamily fontFamily = new FontFamily("Arial");

            int pos = 0;
           Debug.WriteLine("DECKPOSITION> " + item.Deckposition);
            switch (item.Deckposition)
            {
                case 81:
                    pos = 50;
                    break;
                case 17:
                    pos = 25;
                        break;
                case 49:
                    pos = 1;
                    break;
            }
            this.position = pos;
            this.size = item.Decksize;
            this.Color = System.Drawing.ColorTranslator.FromHtml(item.Deckcolor);
            this.text = item.Deckname;
            this.brush = new SolidBrush(this.Color); //Brushes.White;
            this.font = new Font(
   fontFamily,
   16,
   FontStyle.Regular,
   GraphicsUnit.Pixel);
          
        }

        public string Text { get => text; set => text = value; }
        public Brush Brush { get => brush; set => brush = value; }
        public Font Font { get => font; set => font = value; }
        public int Position { get => position; set => position = value; }
        public int Size { get => size; set => size = value; }
        public Color Color { get => color; set => color = value; }
    }
}
