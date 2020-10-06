using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
namespace InterfaceDll
{
    public interface InterfaceDllClass
    {
  
      string GetActionName();

        void Configure(ServiceCollection service);

    }

    public interface IMyService
    {
        Window Execute();
        string Teste();
    }
}
