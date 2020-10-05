using DisplayButtons.Backend.Objects.Implementation.DeckActions.General;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Engine.Wrappers
{
    [MoonSharpUserData]
    public class LibraryInterfaceWrapper
    {
        Assembly AssemblyAcess { get; set; }

        public LibraryInterfaceWrapper(PluginLuaGenerator instance)     
        {
            LoadAssembly(instance.dllpath);
        }
        public void LoadAssembly(string assembly)
        {
            AssemblyAcess = Assembly.LoadFile(Application.StartupPath + "/" + assembly);
        }
      
        public void ExecuteMethod(string assemblyInfo,string method)
        {
            Type assemblyType = AssemblyAcess.GetType(assemblyInfo);

            var o = Activator.CreateInstance(assemblyType, null);
            
        }


    }
}
