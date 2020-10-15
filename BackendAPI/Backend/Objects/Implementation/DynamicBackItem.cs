using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackendAPI.Objects.Implementation
{
    [Serializable]
    public class DynamicBackItem : DynamicDeckItem
    {
        public override DeckImage GetDefaultImage()
        {
            return DeckAction.GetDefaultItemImage();
        }
  

       public override DeckImage GetItemImage()
        {
           return DeckImage;
       }


       


    }
}
