
using InterfaceDll;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

using TransparentTwitchChatWPF;

namespace TransparentTwitchChatWPF.Wrappers
{
   [MoonSharpUserData]
    public class LibraryInterfaceWrapper
    {
        public void ShowChatBox()
        {  
            
          
          
            Thread thread = new Thread(() => {
                new MainWindow().Show();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
      
    }
}
