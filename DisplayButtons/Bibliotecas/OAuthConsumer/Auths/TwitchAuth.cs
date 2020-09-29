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
        private bool _hastoken;
        public bool HasToken()
        {
            return _hastoken;
        }
        public string Token()
        {
            
 return token;
  
        }
        public string Code()
        {

            return code;
        }
        public override async Task AuthCheck()
        { 
            if (File.Exists(CredentialsPath))
            {

                string result = File.ReadAllText(CredentialsPath);
                var json =  JsonConvert.DeserializeObject<JsonOAuthSession>(result);
             
  code = json.code;
                token = json.access_token;
                
              
              
            }
            else
            {
                await Authenticator();
                
            }
          
        }
       
        public override async Task Authenticator()
        {
            WebService.StartWebServer(ServicesEnum.Twitch);
      
            List<string> scope = new List<string>();
            scope.Add("chat:edit");
            scope.Add("chat:read");
            scope.Add("channel:moderate");
            scope.Add("whispers:read");
            scope.Add("whispers:edit");
            scope.Add("channel:manage:extensions");
            scope.Add("bits:read");
            scope.Add("channel:edit:commercial");
            scope.Add("channel:manage:broadcast");
            scope.Add("channel:read:subscriptions");
            scope.Add("clips:edit");
            scope.Add("user:edit");
            scope.Add("user:edit:follows"); 
            scope.Add("user:read:broadcast");
            scope.Add("user:read:email");

            Scopes myscope = new Scopes(scope);

            Url myurl = new Url(ServiceUrl(), ClientId(), RedirectUrl() + "callback", myscope.Scope);


          
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

 JsonOAuthSession myjson = new JsonOAuthSession();
            Globals.events.On("EventHandlerLoginAuth", async (e) =>
            {
                // Cast event argrument to your event object
                var obj = (EventHandlerLoginAuth)e;
                code = obj.code;





                var val = await getTwitchData();

                token = val.access_token;
               
                
  myjson.code = obj.code;
myjson.access_token = val.access_token;
              
                myjson.expires_in = val.expires_in;
                myjson.refresh_token = val.refresh_token;
                              
         

              //  TwitchWrapper.TwitchWrapper.StartChat();
  var json = JsonConvert.SerializeObject(myjson);

await File.WriteAllTextAsync(CredentialsPath, json);
                _hastoken = true;
            });




         //   await TaskEx.WaitUntil(HasToken);
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
