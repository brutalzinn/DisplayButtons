using System.ComponentModel;
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
        [ActionPropertyInclude]

        [ActionPropertyDescription("PLUGINGERENCIER_NAME_LABEL")]

        public string Deckname { get => _deckname; set => _deckname = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKHELPERCOLOR")]
        public string Deckcolor { get => _deckcolor; set => _deckcolor = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKITEMSIZE")]
        public int Decksize { get => _decksize; set => _decksize = value; }

        public int Deckposition { get => (int)DeckPosition;}
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKITEMSTROKERADIUS")]
      
        public float Stroke_radius { get => _shadowradius; set => _shadowradius = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKITEMSTROKEDX")]
        public float Stroke_dxtext { get => stroke_dxtext; set => stroke_dxtext = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKITEMSTROKEDY")]
        public float Stroke_Dy { get => dytextfloat; set => dytextfloat = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKHELPERCOLOR")]
        public string Stroke_color { get => stroke_color; set => stroke_color = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKHELPERENABLESTROKE")]
        public bool IsStroke { get => isstroke; set => isstroke = value; }
       
        public bool Ishinttext { get => ishinttext; set => ishinttext = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKITEMBOLDTEXT")]
        public bool Isboldtext { get => isboldtext; set => isboldtext = value; }
        [ActionPropertyInclude]

        [ActionPropertyDescription("DECKITEMNORMALTEXT")]
        public bool Isnormaltext { get => isnormaltext; set => isnormaltext = value; }
        public bool Isitalictext { get => isitalictext; set => isitalictext = value; }
        [XmlIgnoreAttribute]
        public DeckImage SetDefault { get => setDefault; set => setDefault = value; }
        public DeckImage DeckImage { get; set; }

        public DeckImage GetDefaultImage()
        {
            return new DeckImage(Resources.img_item_default);
        }
        public enum Pos
        {
            [Description("baixo")]
            Baixo = 81,
            [Description("meio")]
            Meio = 17,
            [Description("cima")]
            Cima = 49


        }
        [ActionPropertyInclude]
        [ActionPropertyDescription("DESCRIPTIONSCENEVISIBILITY")]
        public Pos DeckPosition { get; set; } = Pos.Baixo;
        public  DeckImage GetItemImage()
        {
            return DeckImage;
        }
    }
}
