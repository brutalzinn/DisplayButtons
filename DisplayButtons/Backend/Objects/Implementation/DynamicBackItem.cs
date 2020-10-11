using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Objects.Implementation
{
    [Serializable]
    public class DynamicBackItem : DynamicDeckItem
    {
        public override DeckImage GetDefaultImage()
        {
            return DeckAction.GetDefaultItemImage();
        }
        public DeckImage GetLayerOneImage { get => base.GetDeckDefaultLayer.DeckImage; set => base.GetDeckDefaultLayer.DeckImage = value; }
       
        public DeckImage DeckImage { get; set; }
    
        public AbstractDeckAction DeckAction { get; set; }

       public override DeckImage GetItemImage()
        {
           return DeckImage;
       }


       


    }
}
