using BackendAPI.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendAPI.Events
{
    public class PluginEvent : Sharpy.IEvent
    {

        public AbstractDeckAction _item;

        public PluginEvent(AbstractDeckAction item)
        {
            _item = item;
        }
    }
}
