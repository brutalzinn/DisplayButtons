using InterfaceDll;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace TransparentTwitchChatWPF
{
   
    public class InterfaceDllWrapper : InterfaceDll.InterfaceDllClass
    {


        public string GetActionName() { return "Chat box v1"; }
        

        public void Configure(ServiceCollection service)
        {
            service.AddSingleton<IMyService>(new Service());
           
        }



        }
   public class Service : IMyService
        {
        
        public string Teste()
        {

            return "Escrevendo no console..";
        }
            public Window Execute()
            {
            return new MainWindow();
            }

        }
}
