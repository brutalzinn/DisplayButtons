using BackendAPI.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendAPI.Sdk
{
 
        public interface ButtonInterface
        {

            string GetActionName();
            void MenuHelper();
            void OnButtonDown(DeckDevice device);
            void OnButtonUp(DeckDevice device);

        }
    
}
