using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer
{
    public abstract class AbstractAuth
    {
        public abstract string ClientId();
        public abstract string ClientSecret();
        public abstract string RedirectUrl();
        public abstract string GetActionName();
        public abstract bool AuthCheck();
        public abstract void Authenticator();
        public abstract void RefreshToken();
        public abstract string ServiceUrl();

    }
}
