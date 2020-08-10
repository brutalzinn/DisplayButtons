using ButtonDeck.Backend.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects
{
    [XmlInclude(typeof(DynamicDeckFolder))]
    [XmlInclude(typeof(DynamicDeckItem))]
    public abstract class IDeckItem
    {
        public string DeckName { get; set; } = "-";
        public string DeckColor { get; set; }= "#FFFFFF";
        public int DeckSize { get; set; } = 30;
        public int DeckPosition { get; set; } = 81;




        public string ToScript { get; set; } = "";
        public abstract DeckImage GetItemImage();

        public virtual DeckImage GetDefaultImage()
        {
            return null;
        }
        [XmlElement("DeckItemName")]
        private string DeckItemName
        {


            get
            { // serialize

                return DeckName;
            }
            set
            { // deserialize

                DeckName = value;

            }


        }
        [XmlElement("DeckItemColor")]
        private string DeckItemColor
        {


            get
            { // serialize

                return DeckColor;
            }
            set
            { // deserialize


                DeckColor = value;



            }


        }
        [XmlElement("DeckItemSize")]
        private int DeckItemSize
        {


            get
            { // serialize

                return DeckSize;
            }
            set
            { // deserialize

                DeckSize = value;



            }


        }

        [XmlElement("DeckItemPosition")]
        private int DeckItemPosition
        {


            get
            { // serialize

                return DeckPosition;
            }
            set
            { // deserialize

                DeckPosition = value;



            }


        }
    }
}