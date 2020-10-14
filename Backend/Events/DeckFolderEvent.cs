using Backend.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Events
{
    public class DeckFolderEvent : Sharpy.IEvent
    {
        public DynamicDeckFolder _folder;

        public DeckFolderEvent(DynamicDeckFolder folder)
        {
            _folder = folder;
        }
    }
}
