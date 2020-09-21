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


        private DeckItemMisc _getdefaultlayer;
        public virtual DeckImage GetItemImage()
        {
            return null;
        }
      
       
       
        public DeckItemMisc GetDeckLayerTwo { get; set; }
        // public DeckItemMISC GetDeckDefaultLayer { get => _GetDeckDefaultLayer; set => _GetDeckDefaultLayer = value; }

        public DeckItemMisc GetDeckDefaultLayer
        {
            get
            {
                if (_getdefaultlayer == null)
                {
                    _getdefaultlayer = new DeckItemMisc();
                    return _getdefaultlayer;
                }
                else
                {
                    //DEFAULT value here. 
                    return _getdefaultlayer;
                }
            }
            set
            {
                _getdefaultlayer = value;
            }
        }
    
    public virtual DeckImage GetDefaultImage()
        {
            return null;
        }

    }
}