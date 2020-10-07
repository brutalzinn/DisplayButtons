
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using McMaster.NETCore.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace DisplayButtons.Bibliotecas.Helpers
{
    public static class PluginsDllHelpers
    {
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
    }
}
