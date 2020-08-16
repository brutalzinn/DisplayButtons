using DisplayButtons.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents
{


    public abstract class AbstractTrigger

    {
        public enum Type{

            Window

        }
        public abstract Type GetActionType();
        public abstract string GetActionName();
        public abstract UserControl OnSelect();


        public virtual bool OnExit()
        {

            return false;
        }
        public abstract void OnExecute();


    }

    public abstract class AbstractAction

    {

        public abstract string GetActionName();
        public abstract UserControl OnSelect();

        public virtual bool OnExit()
        {

            return false;
        }
        public abstract void OnExecute();


    }
}
