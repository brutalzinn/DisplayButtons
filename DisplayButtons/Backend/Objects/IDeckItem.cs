using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Backend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Objects
{
    [XmlInclude(typeof(DynamicDeckFolder))]
    [XmlInclude(typeof(DynamicDeckItem))]
    [XmlInclude(typeof(Profile))]
    [XmlInclude(typeof(MatrizObject))]
    public abstract class IDeckItem
    {
        private string _deckname  = "";
       private string _deckcolor  = "#FFFFFF";
        private int _decksize  = 30;
        private int _deckposition = 81;
  private float _shadowradius = 2.6f;
        private float stroke_dxtext = 1.5f;
        private float dytextfloat = 1.3f;
        private string stroke_color = "#FFFFFF";
        private bool isstroke;
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

        public abstract DeckImage GetItemImage();

        public virtual DeckImage GetDefaultImage()
        {
            return null;
        }

    }
}