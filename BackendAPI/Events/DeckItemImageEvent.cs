using BackendAPI.Bibliotecas;
using BackendAPI.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendAPI.Events
{
    public class DeckItemChangeAllImagePack : Sharpy.IEvent
    {
        public IDictionary<int, DeckItemMisc> toSend = new Dictionary<int, DeckItemMisc>();
        public void AddToQueue(int slot, DeckItemMisc img)
        {
            toSend.Add(slot, img);
        }

    }

    public class DeckItemClearAllImagePack : Sharpy.IEvent
    {
        readonly List<int> toClear = new List<int>();
        
        public void AddToQueue(int slot)
        {
            toClear.Add(slot);
        }

    }
    public class DeckItemSinglePacket : Sharpy.IEvent
    {
        public DeckImage DeckImage { get; set; }
        public int ImageSlot { get; set; }  
        public DeckItemMisc CurrentItem { get; set; }
        public DeckItemSinglePacket(DeckImage deckImage)
        {
            DeckImage = deckImage;
        }

    }
    public class DeckItemClearSinglePacket : Sharpy.IEvent
    {

        public int SlotID { get; set; }

        public DeckItemClearSinglePacket(int slotID)
        {
            SlotID = slotID;
        }

    }
    public class DeckItemChangeOneImage
    {
        public DeckItemMisc CurrentItem { get; set; }
        public DeckImage DeckImage { get; set; }

        public DeckItemChangeOneImage(DeckImage deckImage)
        {
            DeckImage = deckImage;

        }

    }


}
