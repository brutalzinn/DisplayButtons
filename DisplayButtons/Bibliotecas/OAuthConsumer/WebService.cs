using DisplayButtons;
using DisplayButtons.Bibliotecas.OAuthConsumer;
using DisplayButtons.Bibliotecas.OAuthConsumer.Auths;
using DisplayButtons.Bibliotecas.OAuthConsumer.TwitchEvents;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static DisplayButtons.Bibliotecas.OAuthConsumer.Services;

namespace MyService
{
    internal static class WebService
    {
        /// <summary>
        /// The port the HttpListener should listen on
        /// </summary>
        private const int Port = 5000;
        private static ServicesEnum _service;
        /// <summary>
        /// This is the heart of the web server
        /// </summary>
        private static readonly HttpListener Listener = new HttpListener { Prefixes = { $"http://*:{Port}/" } };

        /// <summary>
        /// A flag to specify when we need to stop
        /// </summary>
        private static bool _keepGoing = true;
 
  public static string Script = @"<script>
if(window.location.hash) {
      var hash = window.location.hash.substring(1); //Puts hash in variable, and removes the # character

var xhr = new XMLHttpRequest();
var url = ""settings"";
xhr.open(""POST"", url, true);
xhr.setRequestHeader(""Content-Type"", ""application/json"");
        var data = JSON.stringify({""key"": hash});
xhr.send(data);

  } else {

      // No hash found
  }
</script>";
    
        public static string pageData = 
   "<!DOCTYPE>" +
   "<html>" +
   "  <head>" +
   "    <title>HttpListener Example</title>" +
   "  </head>" +
   "  <body>" +
   Script + 
   "    <p>Page Views: {0}</p>" +
   "    <form method=\"post\" action=\"shutdown\">" +
   "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
   "    </form>" +
   "  </body>" +
   "</html>";
      


        /// <summary>
        /// Keep the task in a static variable to keep it alive
        /// </summary>
        private static Task _mainLoop;

        /// <summary>
        /// Call this to start the web server
        /// </summary>
        public static void StartWebServer(ServicesEnum service)
        {
            _service = service;
            if (_mainLoop != null && !_mainLoop.IsCompleted) return; //Already started
            _mainLoop = MainLoop();
        }

        /// <summary>
        /// Call this to stop the web server. It will not kill any requests currently being processed.
        /// </summary>
        public static void StopWebServer()
        {
            _keepGoing = false;
            lock (Listener)
            {
                //Use a lock so we don't kill a request that's currently being processed
                Listener.Stop();
            }
            try
            {
                _mainLoop.Wait();
            }
            catch { /* je ne care pas */ }
        }

        /// <summary>
        /// The main loop to handle requests into the HttpListener
        /// </summary>
        /// <returns></returns>
        private static async Task MainLoop()
        {
            Listener.Start();
            while (_keepGoing)
            {
                try
                {
                    //GetContextAsync() returns when a new request come in
                    var context = await Listener.GetContextAsync();
                    lock (Listener)
                    {
                        if (_keepGoing) ProcessRequest(context);
                    }
                }
                catch (Exception e)
                {
                    if (e is HttpListenerException) return; //this gets thrown when the listener is stopped
                    //TODO: Log the exception
                }
            }
        }

        /// <summary>
        /// Handle an incoming request
        /// </summary>
        /// <param name="context">The context of the incoming request</param>
        private static void ProcessRequest(HttpListenerContext context)
        {
            using (var response = context.Response)
            {
                try
                {
                    var handled = false;
                    switch (context.Request.Url.AbsolutePath)
                    {
                        //This is where we do different things depending on the URL
                        //TODO: Add cases for each URL we want to respond to
                        case "/settings":
                            switch (context.Request.HttpMethod)
                            {
                                case "GET":
                                    //Get the current settings
                                    response.ContentType = "application/json";


                                    handled = true;
                                    break;

                                case "PUT":
                                    //Update the settings
                                    using (var body = context.Request.InputStream)
                                    using (var reader = new StreamReader(body, context.Request.ContentEncoding))
                                    {
                                        //Get the data that was sent to us
                                        var json = reader.ReadToEnd();

                                        //Use it to update our settings
                                        //UpdateSettings(JsonConvert.DeserializeObject<MySettings>(json));

                                        //Return 204 No Content to say we did it successfully
                                        response.StatusCode = 204;
                                        handled = true;
                                    }
                                    break;

                                case "POST":
                                    //Get the current settings
                                    using (var body = context.Request.InputStream)
                                    using (var reader = new StreamReader(body, context.Request.ContentEncoding))
                                    {
                                        //Get the data that was sent to us


                                        var json = JsonConvert.DeserializeObject<JsonOAuthSession>(reader.ReadToEnd());
                                        try
                                        {
                                            //    HandlerApi(_service, json.key);
                                            ProcessTypeKey(_service, json.code);
                                        }
                                        catch (Exception)
                                        {

                                        }

                                        //User user = JsonConvert.DeserializeObject<User>(fixedData);

                                        //Use it to update our settings
                                        //UpdateSettings(JsonConvert.DeserializeObject<MySettings>(json));

                                        //Return 204 No Content to say we did it successfully
                                        response.StatusCode = 204;
                                        handled = true;
                                    }
                                    break;

                                    //This is what we want to send back

                                    //  //Write it to the response stream
                                    //  var buffer = Encoding.UTF8.GetBytes(responseBody);
                                    //  response.ContentLength64 = buffer.Length;
                                    //  response.OutputStream.Write(buffer, 0, buffer.Length);


                            }


                            break;

                        case "/callback":
                            switch (context.Request.HttpMethod)
                            {
                               
                                case "GET":
                                    using (var body = context.Request.InputStream)
                                    using (var reader = new StreamReader(body, context.Request.ContentEncoding))
                                    {
                                        byte[] script = Encoding.UTF8.GetBytes(pageData);

                                        response.ContentType = "text/html";
                                        response.ContentEncoding = Encoding.UTF8;
                                        response.ContentLength64 = script.LongLength;

                                        response.OutputStream.WriteAsync(script, 0, script.Length);

                                        try
                                        {
                                            ProcessTypeCode(_service, context.Request.Url.PathAndQuery.ToString());


                                        }
                                        catch (Exception)
                                        {

                                        }
                                        response.StatusCode = 204;

                                        break;

                                    }

                            }
                            break;
                        case "/callapi":
                            switch (context.Request.HttpMethod)
                            {

                                case "GET":
                                    using (var body = context.Request.InputStream)
                                    using (var reader = new StreamReader(body, context.Request.ContentEncoding))
                                    {
                                        byte[] script = Encoding.UTF8.GetBytes(pageData);

                                        response.ContentType = "text/html";
                                        response.ContentEncoding = Encoding.UTF8;
                                        response.ContentLength64 = script.LongLength;

                                        response.OutputStream.WriteAsync(script, 0, script.Length);

                                        try
                                        {
                                            ProcessTypeCode(_service, context.Request.Url.PathAndQuery.ToString());



                                        }
                                        catch (Exception)
                                        {

                                        }
                                        response.StatusCode = 204;

                                        break;

                                    }

                            }
                            break;


                    }





                    if (!handled)
                    {
                        response.StatusCode = 404;
                    }
                }


                catch (Exception e)
                {
                    //Return the exception details the client - you may or may not want to do this
                    response.StatusCode = 500;
                    response.ContentType = "application/json";
                    var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);

                    //TODO: Log the exception
                }
            }
        }
        private static void ProcessTypeKey(ServicesEnum _service_enum, string urlparam)
        {
            var acess_link = HttpUtility.ParseQueryString(urlparam);    
                      string token = acess_link.Get("access_token");
                // string type = acess_link.Get("token_type");
                  
                   Globals.events.Trigger("EventHandlerLoginAuthGetToken", new EventHandlerLoginAuthGetToken(token));
                   
        }
        private static void ProcessTypeCode(ServicesEnum _service_enum, string urlparam)
        {
            var acess_link = HttpUtility.ParseQueryString(urlparam);
            //   string token = acess_link.Get("access_token");
            //     string type = acess_link.Get("token_type");
            string code = acess_link.Get("/callback?code");
            Globals.events.Trigger("EventHandlerLoginAuth", new EventHandlerLoginAuth(code));

        }
        private static void HandlerApi(Action method)
        {

            //switch (_service_enum)
            //{
            //    case ServicesEnum.Twitch:

            //        break;
            //    case ServicesEnum.Default:

            //        break;
            //}
        }
        //private static void UpdateSettings(MySettings newSettings)
        //{
        //    //TODO: Update application settings
        //}

    }
}