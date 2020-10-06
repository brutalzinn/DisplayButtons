using DisplayButtons.Backend.Objects.Implementation.DeckActions.General;
using DisplayButtons.Bibliotecas.Helpers;
using InterfaceDll;
using McMaster.NETCore.Plugins;
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
using System.Windows.Forms;


namespace DisplayButtons.Engine.Wrappers
{
    [MoonSharpUserData]
    public static class LibraryInterfaceWrapper
    {
        public static void ShowChatBox()
        {  
            
          
            var services = new ServiceCollection();

         
           ConfigureServices(services, Globals.loaders);

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IMyService consumer = serviceProvider.GetRequiredService<IMyService>();
            Thread thread = new Thread(() => {
                consumer.Execute().Show();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
         
        }
        public static void ConfigureServices(ServiceCollection services, List<PluginLoader> loaders)
        {
            // Create an instance of plugin types
            foreach (var loader in loaders)
            {
                foreach (var pluginType in loader
                    .LoadDefaultAssembly()
                    .GetTypes()
                    .Where(t => typeof(InterfaceDll.InterfaceDllClass).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    // This assumes the implementation of IPluginFactory has a parameterless constructor
                    var plugin = Activator.CreateInstance(pluginType) as InterfaceDll.InterfaceDllClass;
                    Debug.WriteLine($"Service plugin instance '{plugin.GetActionName()}' created.");

                    plugin?.Configure(services);
                }
            }
        }
        public static void CloseChatBox()
        {
            
        }
    }
}
