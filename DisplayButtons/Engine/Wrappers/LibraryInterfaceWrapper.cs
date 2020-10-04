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
        public LibraryInterfaceWrapper(string assembly)
        {
            AssemblyAcess = Assembly.LoadFile(Application.StartupPath + "/" + assembly);
        }
        public void ButtonDown(string assmblynamespace)
        {
            ExecuteMethod(assmblynamespace).ButtonDown();
      


        }
        public InterfaceDll.InterfaceDllClass ExecuteMethod(string assemblyInfo)
        {
            Type assemblyType = AssemblyAcess.GetType(assemblyInfo);
            if (assemblyType != null)
            {
                Type[] argTypes = new Type[] { };

                ConstructorInfo cInfo = assemblyType.GetConstructor(argTypes);

                InterfaceDll.InterfaceDllClass shippingClass = (InterfaceDll.InterfaceDllClass)cInfo.Invoke(null);

                return shippingClass;
            }
            else
            {
                // Error checking is needed to help catch instances where
                throw new NotImplementedException();
            }

        }

       
    }
}
