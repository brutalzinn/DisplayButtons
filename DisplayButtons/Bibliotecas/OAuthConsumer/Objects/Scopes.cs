using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer.Objects
{
    public class Scopes
    {

        private string scopes;
        public Scopes(List<string> _scopes)
        {
           
                scopes += String.Join(" ", _scopes.ToArray());

            

        }

        public string Scope { get => scopes; set => scopes = value; }
    }
}
