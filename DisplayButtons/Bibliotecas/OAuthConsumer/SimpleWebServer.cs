using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using static DisplayButtons.Bibliotecas.OAuthConsumer.Services;
using DisplayButtons;
using DisplayButtons.Bibliotecas.OAuthConsumer.TwitchEvents;
using System.Web;

namespace SimpleWebServer
{
    /// <summary>
    /// Taken from gist at : https://gist.github.com/aksakalli/9191056
    /// </summary>
    class SimpleHTTPServer
    {
        public readonly string[] _indexFiles = {
            "index.html",
            "index.htm",
            "default.html",
            "default.htm",
            "SandboxLoaderWJS.htm"
        };

        private Thread _serverThread;
        private string _rootDirectory;
        private HttpListener _listener;
        private int _port;
        private ServicesEnum _service;
        private bool localMode = true;
        private bool custom = false;
        public static int pageViews = 0;
        private bool _enabled = false;
        public static int requestCount = 0;

        private List<string> customPrefixes;
        public static string pageData =
    "<!DOCTYPE>" +
    "<html>" +
    "  <head>" +
    "    <title>HttpListener Example</title>" +
    "  </head>" +
    "  <body>" +
    "    <p>Page Views: {0}</p>" +
    "    <form method=\"post\" action=\"shutdown\">" +
    "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
    "    </form>" +
    "  </body>" +
    "</html>";
        public int Port
        {
            get { return _port; }
            private set { }
        }

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        public SimpleHTTPServer( int _port, ServicesEnum service)
        {
       

            this.Initialize(_port, service);
        }

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        /// <param name="local">Indicates whether this is loopback only server</param>
        public SimpleHTTPServer(ServicesEnum service, bool local, bool custom, List<string> customPrefixes)
        {
            this.localMode = local;
            this.custom = custom;
            this._service = service;
            this.customPrefixes = customPrefixes;
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize(port, service);

        }

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        public SimpleHTTPServer(string path)
        {
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize(port, ServicesEnum.Default);
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
          _enabled = false;
            _listener.Close();
            //   _listener.Close();
            //  _listener.Stop();
            
       _serverThread.Interrupt();

         
        }

        private void Listen()
        {
            _listener = new HttpListener();
            
            // Allow user to override prefixes via config file
            if (this.custom)
            {
                foreach (string prefix in this.customPrefixes)
                {
                    _listener.Prefixes.Add(prefix);
                }
            }
            else
            {
           
 _listener.Prefixes.Add("http://" + (localMode ? "localhost" : "*") + ":" + _port.ToString() + "/");
                
               
            }

          

            try
            {
                _listener.Start();
            }
            catch (Exception)
            {
                
               
                //  throw new Exception(string.Format("Failed to start listener for {0} make sure that you have admin privileges",ex));
            }


            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Process(context);
                }
                catch (Exception)
                {

                }
            }
        }
        private void ProcessTypeRequest(ServicesEnum _service_enum, HttpListenerRequest req)
        {
            var acess_link = HttpUtility.ParseQueryString(req.Url.Query);

            switch (_service_enum)
            {

                case ServicesEnum.Twitch:

                    //Debug.WriteLine("Link" + req.Url.Query.ToString());
                    //string token = acess_link.Get("access_token");
                    //string type = acess_link.Get("token_type");
                    //Globals.events.Trigger("TwitchEventHandlerLoginOauth", new EventHandlerLoginAuth(token, type));
                 
                    break;
                case ServicesEnum.Default:

                    Debug.WriteLine("Link default" + acess_link);
                    break;
            }
      

        }
        private void Process(HttpListenerContext context)
        {

            try
            {


                HttpListenerRequest req = context.Request;
                HttpListenerResponse resp = context.Response;

                // Print out some info about the request
                Debug.WriteLine("Request #: {0}", ++requestCount);
                Debug.WriteLine(req.Url.ToString());
                Debug.WriteLine(req.HttpMethod);
                Debug.WriteLine(req.UserHostName);
                Debug.WriteLine(req.UserAgent);
                

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");

                }


             





                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // Write the response info

                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, ""));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                resp.OutputStream.WriteAsync(data, 0, data.Length); 
               
                resp.Close();
                if (req.Url.AbsolutePath == "/callback")
                {
                    ProcessTypeRequest(_service, req);

                }




            }
            catch (Exception)
            {

            }


                
          
       }

        private void Initialize( int port, ServicesEnum service)
        {
            
            this._port = port;
            this._service = service;
            this._enabled = true;
            _serverThread = new Thread(this.Listen);
            _serverThread.Start();
        }

    }
}