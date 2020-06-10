using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects.Implementation
{
    [Serializable]
    public class DynamicDeckItem : IDeckItem
    {
        public override DeckImage GetDefaultImage()
        {
            return DeckAction.GetDefaultItemImage();
        }

        public DeckImage DeckImage { get; set; }
        public string DeckName { get; set; }
        public string DeckColor { get; set; }
        public AbstractDeckAction DeckAction { get; set; }
        public AbstractDeckInformation DeckInformation { get; set; }
        public override DeckImage GetItemImage()
        {
            return DeckImage;
        }
        [XmlElement("DeckItemName")]
        private string DeckItemName
        {


            get
            { // serialize
                if (DeckName == null) return null;
                return DeckName;
            }
            set
            { // deserialize
                if (value == null)
                {
                    DeckName = null;
                }
                else
                {
                    DeckName = value;

                }

            }


        }
        [XmlElement("DeckItemColor")]
        private string DeckItemColor
        {


            get
            { // serialize
                if (DeckColor == null) return null;
                return DeckColor;
            }
            set
            { // deserialize
                if (value == null)
                {
                    DeckColor = null;
                }
                else
                {
                    DeckColor = value;

                }

            }


        }
    }
}
