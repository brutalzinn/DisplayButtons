using DisplayButtons.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Objects
{
   public class DeckItemMisc
    {

        private string _deckname = "";
        private string _deckcolor = "#FFFFFF";
        private int _decksize = 20;
        private int _deckposition = 81;
        private float _shadowradius = 2.6f;
        private float stroke_dxtext = 1.5f;
        private float dytextfloat = 1.3f;
        private string stroke_color = "#FFFFFF";
        private bool isstroke;
        private bool ishinttext = false;
        private bool isboldtext = false;
        private bool isnormaltext = true;
        private bool isitalictext = false;

        private DeckImage setDefault;
        public string ToScript { get; set; } = "";
        public string Deckname { get => _deckname; set => _deckname = value; }
        public string Deckcolor { get => _deckcolor; set => _deckcolor = value; }
        public int Decksize { get => _decksize; set => _decksize = value; }
        public int Deckposition { get => _deckposition; set => _deckposition = value; }
        public float Stroke_radius { get => _shadowradius; set => _shadowradius = value; }
        public float Stroke_dxtext { get => stroke_dxtext; set => stroke_dxtext = value; }
        public float Stroke_Dy { get => dytextfloat; set => dytextfloat = value; }
        public string Stroke_color { get => stroke_color; set => stroke_color = value; }
        public bool IsStroke { get => isstroke; set => isstroke = value; }
        public bool Ishinttext { get => ishinttext; set => ishinttext = value; }
        public bool Isboldtext { get => isboldtext; set => isboldtext = value; }
        public bool Isnormaltext { get => isnormaltext; set => isnormaltext = value; }
        public bool Isitalictext { get => isitalictext; set => isitalictext = value; }
        [XmlIgnoreAttribute]
        public DeckImage SetDefault { get => setDefault; set => setDefault = value; }
        public DeckImage DeckImage { get; set; }

        public DeckImage GetDefaultImage()
        {
            return new DeckImage(Resources.img_item_default);
        }
      

        public  DeckImage GetItemImage()
        {
            return DeckImage;
        }
    }
}
