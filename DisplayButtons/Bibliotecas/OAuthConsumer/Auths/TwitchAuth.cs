using DisplayButtons.Bibliotecas.OAuthConsumer.Objects;
using DisplayButtons.Bibliotecas.OAuthConsumer.TwitchEvents;
using MyService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static DisplayButtons.Bibliotecas.OAuthConsumer.Services;

namespace DisplayButtons.Bibliotecas.OAuthConsumer.Auths
{
    public class TwitchAuth : AbstractAuth
    {
        private const string CredentialsPath = "credentials_twitch.json";
        public string code;
        private string token;



        public override bool AuthCheck()
        { 
            if (File.Exists(CredentialsPath))
            {
                return true;
            }
            else
            {
                Authenticator();
                return false;
            }
          
        }

        public override void Authenticator()
        {
            WebService.StartWebServer(ServicesEnum.Twitch);

            List<string> scope = new List<string>();
            scope.Add("user:read:broadcast");
            scope.Add("channel:edit:commercial");
            scope.Add("user:edit");

            Scopes myscope = new Scopes(scope);

            Url myurl = new Url(ServiceUrl(), ClientId(), RedirectUrl() + "callback", myscope.Scope);

            Debug.WriteLine("URL LINK GET CODE : " + myurl.Url_feeder);
            Uri uri = (new Uri(myurl.Url_feeder));
            try
            {
                BrowserUtil.Open(uri);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to open URL, manually open: {0}", uri);
            }


            Globals.events.On("EventHandlerLoginAuth", (e) =>
            {
                // Cast event argrument to your event object
                var obj = (EventHandlerLoginAuth)e;
                code = obj.code;
                JavaScriptOauthInfo myjson = new JavaScriptOauthInfo();

                myjson.code = obj.code;

                var json = JsonConvert.SerializeObject(myjson);


                File.WriteAllTextAsync(CredentialsPath, json);
                var result = Task.FromResult(getTwitchData()).Result;
                Debug.WriteLine("KEY IS : " + result.ToString()) ;
                


              
            });

                // Cast event argrument to your event object

                Globals.events.On("EventHandlerLoginAuthGetToken", (e) =>
            {
                var obj = (EventHandlerLoginAuthGetToken)e;


                Debug.WriteLine("MEU TOKEN é " + obj.token);


            });







            
            }
  
       
        public override string ClientId()
        {
            return "h8ocjqn8n6fvv4jrr6oyzb0f923v7e";
       }

       

        public override string GetActionName()
        {
            return "Twitch Authenticator";
        }

        public override string RedirectUrl()
        {
            return "http://localhost:5000/";
        }

        public override void RefreshToken()
        {
            throw new NotImplementedException();
        }

        public override string ServiceUrl()
        {
            return "https://id.twitch.tv/oauth2/authorize";
        }

        public override string ClientSecret()
        {
            return "sx26x69d1ak429bed15b737589u020";
        }
        public async Task<JObject> getTwitchData()
        {
            var httpClientRequest = new HttpClient();

            //var postData = new Dictionary<string, object>();
            //postData.Add("client_id", "ihjl5qbxnatduno6yrs1h0x0eei40g");
            //postData.Add("client_secret", "3ppn2pgdhuv8j5gfrmlr980binpg09");
            //postData.Add("code", code);
            //postData.Add("grant_type", "authorization_code");
            //postData.Add("redirect_uri", "http://localhost:5000/callapi");

            // postData.Add("state", mycreatedtoken);
            List<string> scope = new List<string>();
            scope.Add("user:read:broadcast");
            scope.Add("channel:edit:commercial");
            scope.Add("user:edit");
          

            //    var jsonRequest = JsonConvert.SerializeObject(postData);
            // HttpContent content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            Url my_post = new Url("https://id.twitch.tv/oauth2/token", ClientId(), ClientSecret(), code, "authorization_code", RedirectUrl() + "callapi");

            var result = await httpClientRequest.PostAsync(my_post.Url_feeder,null);
            try
            {
                var resultString = await result.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(resultString);
                System.Diagnostics.Debug.WriteLine(jsonResult);
                return jsonResult;
            }

            catch
            {
                System.Diagnostics.Debug.WriteLine(result);
                return null;
            }
        }
    }
}
