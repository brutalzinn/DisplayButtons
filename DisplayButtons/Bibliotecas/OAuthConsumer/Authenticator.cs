using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DisplayButtons.Bibliotecas.OAuthConsumer
{
   public static class AuthenticatorHelper
    {

        public static void UpdateJson(string input_json_read, string new_json,string namepath )
        {

JsonOAuthSession myjson = new JsonOAuthSession();
             var jsonResult = JsonConvert.DeserializeObject<JsonOAuth>(input_json_read);
            
       
            myjson.access_token = jsonResult.access_token;

            myjson.expires_in = jsonResult.expires_in;
            myjson.refresh_token = jsonResult.refresh_token;

            var json = JsonConvert.SerializeObject(myjson);

            File.WriteAllTextAsync(namepath,json);

        }

    }
}
