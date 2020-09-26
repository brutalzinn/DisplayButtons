using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer
{
   
        public class JavaScriptOauthInfo
        {
   
            public string key { get; set; }
        public string code { get; set; }
        public string auth { get; set; }
    }
    public class JsonAuthTwitch
    {

        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string [] scope { get; set; }
        public string token_type { get; set; }
    }

}
