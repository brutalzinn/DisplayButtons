using BackendAPI.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendAPI.Events
{
   public class IDeckInterfaceEvent : Sharpy.IEvent
    {

        public IDeckItem _item;
        public DeckDevice _device;
        public int _mode;

        public IDeckInterfaceEvent(IDeckItem item, DeckDevice device, int mode)
        {
            _item = item;
            _device = device;
            _mode = mode;
        }
    }
}
