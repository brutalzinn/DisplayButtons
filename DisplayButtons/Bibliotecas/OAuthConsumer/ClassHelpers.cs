using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer
{
   
        public class JsonOAuthSession
        {
   
            public string access_token { get; set; }
        public string code { get; set; }
        public int expires_in { get; set; }
        public bool auth { get; set; }
        public string refresh_token { get; set; }
    }
    public class JsonOAuth
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public List<string> scope { get; set; }
        public string token_type { get; set; }
    }

}
