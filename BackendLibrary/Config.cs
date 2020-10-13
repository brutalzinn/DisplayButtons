using NthDeveloper.MultiLanguage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BackendLibrary
{
    public class Config
    {
        public static int mode = 0;
        public static IMultiLanguageProvider m_LanguageProvider;
        public static void SetLang(string lang)
        {
            string directory = Path.GetFullPath($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\langs");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }



            XmlFileSource _xmlFileSource = new XmlFileSource(directory);
            MultiLanguageProvider _languageProvider = new MultiLanguageProvider(_xmlFileSource);



            _languageProvider.SetCurrentLanguage(lang);

            Config.m_LanguageProvider = _languageProvider;
        }
    }
}
