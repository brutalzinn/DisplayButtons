using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using MoonSharp.Interpreter;
using Backend.Objects;

namespace InterfaceDll
{
    public interface InterfaceDllClass
    {
  
      string GetActionName();

        void SetLang(string lang);

        void Configure(ServiceCollection service);
        void LoadScripts(Script scripter);
   

    }

    public interface ButtonInterface
    {

        string GetActionName();
        void MenuHelper();
        void OnButtonDown(DeckDevice device);
        void OnButtonUp(DeckDevice device);

    }
    public interface IMyService
    {
      
      
        string Teste();
    }
}
