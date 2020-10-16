using BackendAPI.Objects;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BackendAPI.Utils
{
    class XMLUtils
    {

        protected static Type[] extraTypes;

        public static Type[] ExtraTypes{
       get
            {
                if (extraTypes == null)
                    extraTypes = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>().Select(c => c.GetType()).ToArray();
                return extraTypes;
            }
        }
//       public static Type[] types = new Type[] { typeof(AbstractDeckAction) };


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
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), ExtraTypes);
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
     
        

    }
}
