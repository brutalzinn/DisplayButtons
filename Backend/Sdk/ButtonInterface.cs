using Backend.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Sdk
{
 
        public interface ButtonInterface
        {

            string GetActionName();
            void MenuHelper();
            void OnButtonDown(DeckDevice device);
            void OnButtonUp(DeckDevice device);

        }
    
}
