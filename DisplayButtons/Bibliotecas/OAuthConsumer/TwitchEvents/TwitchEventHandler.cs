using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer.TwitchEvents
{
    
        public class TwitchEventHandlerLoginOauth : Sharpy.IEvent
        {

            public string token;
            public string type;

            public TwitchEventHandlerLoginOauth(string _token, string _type)
            {
                this.token = _token;
            this.type = _type;
            }
        }
    
}
