using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    public abstract class AbstractAction

    {

        public abstract string GetActionName();
        public abstract void OnExecute();
        public abstract void ControlsConstrutor();

    }
}
