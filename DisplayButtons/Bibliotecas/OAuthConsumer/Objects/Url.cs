using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer.Objects
{
    public class Url
    {
        private string url_feeder;

        public Url(string service_url,string clientid,string redirecturl, string scope)
        {
            this.url_feeder =  String.Format("{0}?client_id={1}&redirect_uri={2}&response_type=code&scope={3}", service_url,clientid, redirecturl, scope);
           
        }
        public Url(string service_url, string clientid, string clientsecret, string code, string grant_type,string redirect_url)
        {
            this.url_feeder = String.Format("{0}?client_id={1}&client_secret={2}&code={3}&response_type=token&grant_type={4}&redirect_uri={5}", service_url, clientid, clientsecret, code, grant_type, redirect_url);
     
  
        }
        public string Url_feeder { get => url_feeder; set => url_feeder = value; }
    }
}
