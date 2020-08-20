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
        public abstract PanelControl OnSelect();
        public abstract AbstractTrigger CloneAction();
      

        public virtual bool OnExit()
        {

            return false;
        }
        public abstract void OnInit();
        public abstract void OnExecute();

        public bool Equals(AbstractTrigger other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return GetActionName().Equals(other.GetActionName());
        }
        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashProductName = GetActionName() == null ? 0 : GetActionName().GetHashCode();

            //Get hash code for the Code field.

            return hashProductName;
            //Calculate the hash code for the product.

        }
    }

    public abstract class AbstractAction

    {

        public abstract string GetActionName();
        public abstract PanelControl OnSelect();
        public abstract AbstractAction CloneAction();
        public virtual bool OnExit()
        {

            return false;
        }
        public abstract void OnInit();
        public abstract void OnExecute();


    }
}
