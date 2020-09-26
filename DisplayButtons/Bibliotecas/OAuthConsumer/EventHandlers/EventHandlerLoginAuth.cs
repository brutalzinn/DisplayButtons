using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer.TwitchEvents
{

    public class EventHandlerLoginAuth : Sharpy.IEvent
    {

    
        public string code;
        public EventHandlerLoginAuth( string _code)
        {
         
            this.code = _code;
        } 
      
        

    }
    public class EventHandlerLoginAuthGetToken : Sharpy.IEvent
    {

        public string token;
    
        public EventHandlerLoginAuthGetToken(string _token)
        {
            this.token = _token;
     
         
        }

      


    }





}
