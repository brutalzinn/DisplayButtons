using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons
{
   public class LibraryHelper
    {
       public  List<LibraryInfo> toAdd = new List<LibraryInfo>();
        public void prepareLibraryList()
        {

            LibraryInfo dotnetzip =  new LibraryInfo("dotnetzip", "https://github.com/DinoChiesa/DotNetZip");
            LibraryInfo MoonSharp = new LibraryInfo("MoonSharp", "https://github.com/moonsharp-devs/moonsharp");
            LibraryInfo shortid = new LibraryInfo("dotnetzip", "https://github.com/bolorundurowb/shortid");
            LibraryInfo Tesseract = new LibraryInfo("Tesseract", "https://github.com/charlesw/tesseract");
            LibraryInfo SharpAdbClient = new LibraryInfo("SharpAdbClient", "https://github.com/quamotion/madb");
            toAdd.Add(dotnetzip);
            toAdd.Add(MoonSharp);
            toAdd.Add(shortid);
            toAdd.Add(Tesseract);
            toAdd.Add(SharpAdbClient);
           

        }

    }
}
