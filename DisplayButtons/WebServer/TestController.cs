using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebShard
{

    public sealed class HelloWorldController
    {   
        private readonly IHttpRequestContext _request;
        public class Model
        {
            public string buttonid { get; set; }
         

            public Model()
            {

            }

            public Model(string _buttonid)
            {
                buttonid = _buttonid;
    
            }
        }
        public  HelloWorldController(IHttpRequestContext request)
        {
            _request = request;
          
        }
  
        public IResponse Abacate(Model model)
        {
            Debug.WriteLine("ACTION: "+ model.buttonid);

            return new RedirectResponse("/");
        }

        public IResponse Post(string model)
        {
        
            return new RedirectResponse("/");
        }
        public IResponse Get()
        {
            //   return new ContentResponse("Hello World!");
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                       Debug.WriteLine(path + @"\Content\Index.html");

            return new FileSystemResponse(path + @"\Content\Index.html", "text/html", "utf-8");
        }
        public IResponse JsonTest()
        {
            return new JsonResponse(new { Foo = 123, Bar = new[] { 1, 2, 3 } });
        }
    }
}