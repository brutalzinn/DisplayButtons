using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Backend.Objects.Implementation
{
    [Serializable]
    public class DynamicDeckItem : IDeckItem
    {
        public override DeckImage GetDefaultImage()
        {
            return DeckAction.GetDefaultItemImage();
        }
        [XmlIgnore]
        public DeckImage GetLayerOneImage { get => base.GetDeckDefaultLayer.DeckImage; set => base.GetDeckDefaultLayer.DeckImage = value; }
      
        [XmlIgnore]
        public DeckImage GetLayerTwoImage { get => base.GetDeckLayerTwo.DeckImage; set => base.GetDeckLayerTwo.DeckImage = value; }

        public DeckImage DeckImage { get; set; }
    
        public AbstractDeckAction DeckAction { get; set; }

       public override DeckImage GetItemImage()
        {
           return DeckImage;
       }


       


    }
}
