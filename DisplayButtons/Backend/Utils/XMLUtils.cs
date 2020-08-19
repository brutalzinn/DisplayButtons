using DisplayButtons.Backend.Objects;
using DisplayButtons.Bibliotecas.DeckEvents;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DisplayButtons.Backend.Utils
{
    class XMLUtils
    {

        protected static Type[] extraTypes;
        protected static Type[] extraTypesTrigger;
        protected static Type[] extraTypesAction;
        public static Type[] ExtraTypes {
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
                if (extraTypes == null)
                    extraTypesTrigger = ReflectiveEnumerator.GetEnumerableOfType<AbstractTrigger>().Select(c => c.GetType()).ToArray();
                extraTypesAction = ReflectiveEnumerator.GetEnumerableOfType<AbstractAction>().Select(c => c.GetType()).ToArray();
                extraTypes.Concat(extraTypesTrigger);
                extraTypes.Concat(extraTypesAction);
                return extraTypes;
            }
        }
        public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml)) {
                XmlSerializer serializer = new XmlSerializer(typeof(T), ExtraTypes);
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder())) {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), extraTypesTrigger);
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
        public static string ToXmlEvents<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), ExtraTypesEvents);
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

    }
}
