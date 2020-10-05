using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DisplayButtons
{
	public static class Plugin
	{
		public static T CreatePlugin<T>(string file)
		{

			T plugin = default(T);

			Type pluginType = null;

			if (File.Exists(file))
			{
				Assembly asm = Assembly.LoadFile(file);

				if (asm != null)
				{
					for (int i = 0; i < asm.GetTypes().Length; i++)
					{
						Type type = (Type)asm.GetTypes().GetValue(i);

						if (IsImplementationOf(type, typeof(InterfaceDll.InterfaceDllClass)))
						{
							plugin = (T)Activator.CreateInstance(type);
						}
					}
				}
			}

			return plugin;
		}

		private static bool IsImplementationOf(Type type, Type @interface)
		{
			Type[] interfaces = type.GetInterfaces();

			return interfaces.Any(current => IsSubtypeOf(ref current, @interface));
		}
		private static bool IsSubtypeOf(ref Type a, Type b)
		{
			if (a == b)
			{
				return true;
			}

			if (a.IsGenericType)
			{
				a = a.GetGenericTypeDefinition();

				if (a == b)
				{
					return true;
				}
			}

			return false;
		}
	}
}
