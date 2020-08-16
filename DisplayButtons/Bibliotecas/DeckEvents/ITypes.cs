using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    class ITypes
    {

        public enum Type
        {

            Trigger,
            Action
        }
        public enum EventType
        {
            Window,
            Process,
            Clipboard,
            Mouse,
            Keyboard
        }

    }
}
