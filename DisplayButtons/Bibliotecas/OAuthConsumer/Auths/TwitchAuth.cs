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
        private string code;
        private string token;


        public string Token()
        {
            if(token != null)
            {
 return token;
            }
            else
            {
                return "";
            }
           
        }
        public string Code()
        {

            return code;
        }
        public override bool AuthCheck()
        { 
            if (File.Exists(CredentialsPath))
            {

                string result = File.ReadAllText(CredentialsPath);
                var json =  JsonConvert.DeserializeObject<JsonOAuthSession>(result);
                if(json.expires_in != 0)
                {
  code = json.code;
                token = json.access_token;
                }
              
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
            scope.Add("chat:edit");
            scope.Add("chat:read");
            scope.Add("channel:moderate");
            scope.Add("whispers:read");
            scope.Add("whispers:edit");


            Scopes myscope = new Scopes(scope);

            Url myurl = new Url(ServiceUrl(), ClientId(), RedirectUrl() + "callback", myscope.Scope);

            Debug.WriteLine("MINHA URL COM SCOPE" + myurl.Url_feeder);
          
            try
            {
                try
                {
                    Process myProcess = new Process();
                    // true is the default, but it is important not to set it to false
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = myurl.Url_feeder;
                    myProcess.Start();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            catch (Exception)
            {
                //         Console.WriteLine("Unable to open URL, manually open: {0}", uri);
            }


            Globals.events.On("EventHandlerLoginAuth", (e) =>
            {
                // Cast event argrument to your event object
                var obj = (EventHandlerLoginAuth)e;
                code = obj.code;

             



                var val = Task.Run(() => getTwitchData());
              token = val.Result.access_token ;
                JsonOAuthSession myjson = new JsonOAuthSession();


  myjson.code = obj.code;
myjson.access_token = token;

                myjson.expires_in = val.Result.expires_in;
                myjson.refresh_token = val.Result.refresh_token;
                                var json = JsonConvert.SerializeObject(myjson);
         
File.WriteAllTextAsync(CredentialsPath, json);
                TwitchWrapper.TwitchWrapper.StartChat();


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
        public async Task<JsonOAuth> getTwitchData()
        {
            var httpClientRequest = new HttpClient();

     
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
                var jsonResult = JsonConvert.DeserializeObject<JsonOAuth>(resultString);

               // var jsonResult = JObject.Parse(resultString);
                System.Diagnostics.Debug.WriteLine(jsonResult);
                return jsonResult;
            }

            catch
            {
                System.Diagnostics.Debug.WriteLine(result);
                return null;
            }
        }
        public async Task<JsonOAuth> refreshToken(string refreshtoken)
        {
            var httpClientRequest = new HttpClient();


        

            //    var jsonRequest = JsonConvert.SerializeObject(postData);
            // HttpContent content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            Url my_post = new Url("https://id.twitch.tv/oauth2/token", ClientId(), ClientSecret(), refreshtoken, "refresh_token");

            var result = await httpClientRequest.PostAsync(my_post.Url_feeder, null);
            try
            {
                var resultString = await result.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<JsonOAuth>(resultString);

                // var jsonResult = JObject.Parse(resultString);
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
