using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebShard;

namespace DisplayButtons
{
    public class ResourcesController
    {
        public IResponse Get(string resourceName, string resourcePath)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
          //  Debug.WriteLine(path + @"\Content\Index.html");
            return new FileSystemResponse(path + "/Content/" + resourcePath + "/" + resourceName, resourceName.EndsWith("js") ? "application/javascript" : "application/css", "utf-8");
        }
    }
}
