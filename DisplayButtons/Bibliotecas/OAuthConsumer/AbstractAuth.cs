using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisplayButtons.Bibliotecas.OAuthConsumer
{
    public abstract class AbstractAuth
    {
        public abstract string ClientId();
        public abstract string ClientSecret();
        public abstract string RedirectUrl();
        public abstract string GetActionName();
   
        public virtual async Task AuthCheck() { }
        public virtual async Task Authenticator() { }
        public abstract void RefreshToken();
        public abstract string ServiceUrl();

    }
}
