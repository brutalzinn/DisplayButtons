using BackendAPI.Events;
using BackendAPI.Objects;
using BackendProxy;

using Sharpy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendAPI.Objects
{
    public interface IDeckHelper  
    {

        public static void setVariable(bool variable, IDeckItem item, DeckDevice device)
        {
            try
            {

                if (variable)
                {
                    Wrapper.events.Trigger("IDeckInterfaceEvent", new IDeckInterfaceEvent(item,device,2));

                   // DeckHelpers.RefreshButton(item, 2, item.GetDeckLayerTwo, device);
                }
                else
                {
                    Wrapper.events.Trigger("IDeckInterfaceEvent", new IDeckInterfaceEvent(item, device, 1));
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
