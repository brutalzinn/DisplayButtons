using BackendAPI.Objects;
using McMaster.NETCore.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BackendAPI.Utils
{
    public class XMLUtils
    {
        public static List<AbstractDeckAction> PluginList { get; set; } = new List<AbstractDeckAction>();
      //  public static List<PluginLoader> PluginList =  new List<PluginLoader>();
        protected static Type[] extraTypes;
 
        public static Type[] ExtraTypes{
       get
            {
                if (extraTypes == null)
                    extraTypes = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>().Select(c => c.GetType()).ToArray();
                return extraTypes;
            }
        }
        public static Type[] ExtraTypesEvents
        {

            get
            {
                Type[] result = null;

                extraTypes = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>().Select(c => c.GetType()).ToArray();
                if (LoadActionPlugins != null)
                {
                    result = new Type[extraTypes.Length + LoadActionPlugins.Length];
                    extraTypes.CopyTo(result, 0);
                    LoadActionPlugins.CopyTo(result, extraTypes.Length);
                }
                else
                {
                    result = extraTypes;
                }

                return result;
            }
        }

        public static Type[] LoadActionPlugins
        {
            get
            {
                List<Type> pluginTypes = new List<Type>();

                foreach (AbstractDeckAction pluginPath in PluginList)
                {
                    pluginTypes.Add(pluginPath.GetType());

                }

                return pluginTypes.Count == 0 ? null : pluginTypes.ToArray();
            }
        }
        //       public static Type[] types = new Type[] { typeof(AbstractDeckAction) };


        public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml)) {
                XmlSerializer serializer = new XmlSerializer(typeof(T), ExtraTypesEvents);
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder())) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), ExtraTypesEvents);
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
        public static T SettingsFromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), ExtraTypes);
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public static string SettingsToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), ExtraTypes);
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }


    }
}
