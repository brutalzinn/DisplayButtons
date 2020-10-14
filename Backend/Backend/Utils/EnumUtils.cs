using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Utils
{
    public static class EnumUtils
    {

        public static string GetDescription(Type enumType, Enum enumValue, string defDesc)
        {

            FieldInfo fi = enumType.GetField(enumValue.ToString());

            if (fi != null) {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;

            }

            return defDesc;
        }
        public static string GetDescriptionTranslator(Type enumType, Enum enumValue, string defDesc)
        {

            FieldInfo fi = enumType.GetField(enumValue.ToString());

            if (fi != null)
            {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return Texts.rm.GetString(((DescriptionAttribute)attrs[0]).Description, Texts.cultereinfo);;

            }

            return defDesc;
        }
        public static string GetDescription(Type enumType, Enum enumValue)
        {
            return GetDescription(enumType, enumValue, string.Empty);
        }

        public static string GetDescription<T>(T enumValue, string defDesc)
        {

            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (fi != null) {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return defDesc;
        }

        public static string GetDescription<T>(T enumValue)
        {
            return GetDescription(enumValue, string.Empty);
        }

        public static T FromDescription<T>(string description)
        {
            Type t = typeof(T);
            foreach (FieldInfo fi in t.GetFields()) {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0) {
                    foreach (DescriptionAttribute attr in attrs) {
                        if (attr.Description.Equals(description))
                            return (T)fi.GetValue(null);
                    }
                }
            }
            return (T)Enum.Parse(t, description);
        }


        public static Enum FromDescription(Type enumType, string description)
        {
            Type t = enumType;
            foreach (FieldInfo fi in t.GetFields()) {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0) {
                    foreach (DescriptionAttribute attr in attrs) {
                        if (attr.Description.Equals(description))
                            return (Enum)fi.GetValue(null);
                    }
                }
            }
            return (System.Enum)Enum.Parse(enumType, description);
        }
         public static Enum FromDescriptionTRanslator(Type enumType, string description)
        {
            Type t = enumType;
            foreach (FieldInfo fi in t.GetFields()) {
                object[] attrs = fi.GetCustomAttributes
                        (typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0) {
                    foreach (DescriptionAttribute attr in attrs) {
                        if (attr.Description.Equals(GetResxNameByValue(description)))
                            return (Enum)fi.GetValue(null);
                    }
                }
            }
           
            return (System.Enum)Enum.Parse(enumType, GetResxNameByValue(description));

            // return (System.Enum)Enum.Parse(enumType, description);
        }
        private static string GetResxNameByValue(string value)
        {
           


            var entry =
                Texts.rm.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentCulture, true, true)
                  .OfType<DictionaryEntry>()
                  .FirstOrDefault(e => e.Value.ToString() == value);

            var key = entry.Key.ToString();
            return key;

        }
    }
}
