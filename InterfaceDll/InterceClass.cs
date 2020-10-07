using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using MoonSharp.Interpreter;
namespace InterfaceDll
{
    public interface InterfaceDllClass
    {
  
      string GetActionName();

        void Configure(ServiceCollection service);
        void LoadScripts(Script scripter);
   

    }

    public interface IMyService
    {
      
      
        string Teste();
    }
}
