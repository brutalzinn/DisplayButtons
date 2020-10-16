using BackendAPI.Objects;
using BackendAPI.Utils;
using DisplayButtons.Bibliotecas.DeckEvents;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DisplayButtons.DeckEvents
{
    class XMLDisplayUtils
    {

        protected static Type[] extraTypes;
        protected static Type[] extraTypesTrigger;
        protected static Type[] extraTypesAction;
    
        public static Type[] ExtraTypesEvents
        {

       get
            {
                Type[] result = null;
                
                    extraTypesTrigger = ReflectiveEnumerator.GetEnumerableOfType<AbstractTrigger>().Select(c => c.GetType()).ToArray();
                    extraTypesAction = ReflectiveEnumerator.GetEnumerableOfType<AbstractAction>().Select(c => c.GetType()).ToArray();
                    result = new Type[extraTypesTrigger.Length + extraTypesAction.Length];

                    extraTypesTrigger.CopyTo(result, 0);
                    extraTypesAction.CopyTo(result, extraTypesTrigger.Length);
                
                return result;
            }
        }
        
        public static string ToXmlEvents<T>(T obj)
        {
            XmlWriterSettings XmlSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), ExtraTypesEvents);
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
        public static T FromXMLEvents<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T), ExtraTypesEvents);
                return (T)serializer.Deserialize(stringReader);
            }
        }

    }
}
