using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NthDeveloper.MultiLanguage
{
    class XmlLanguagePackage : ILanguagePackage
    {
        private bool m_IsLoaded;
        private string m_SourceFilePath;        
        private bool m_HasSingleGroup;
        private Dictionary<string, string> m_SingleGroupCache;
        private Dictionary<string, Dictionary<string, string>> m_MultiGroupCache;

        public bool IsLoaded { get { return m_IsLoaded; } }

        public string LanguageCode { get; private set; }

        public string LanguageName { get; private set; }

        private XmlLanguagePackage(string sourceFilePath, string languageCode, string languageName)
        {
            m_SourceFilePath = sourceFilePath;
            this.LanguageCode = languageCode;
            this.LanguageName = languageName;
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", this.LanguageName, this.LanguageCode);
        }

        public string GetString(string key)
        {
            if (m_HasSingleGroup)
                return m_SingleGroupCache[key];

            foreach(string groupName in m_MultiGroupCache.Keys)
            {
                if (m_MultiGroupCache[groupName].ContainsKey(key))
                    return m_MultiGroupCache[groupName][key];
            }

            return key;
        }

        public string GetString(string groupName, string key)
        {
            if (m_HasSingleGroup)
                return m_SingleGroupCache[key];

            return m_MultiGroupCache[groupName][key];
        }

        internal static XmlLanguagePackage LoadOnlyWithNameAndCodeFromFile(string filePath)
        {
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                reader.MoveToContent();

                return new XmlLanguagePackage(filePath, reader.GetAttribute("Code"), reader.GetAttribute("Name"));
            }
        }

        internal void LoadFullData()
        {
            if (m_IsLoaded)
                return;

            XmlLanguageData languageData = null;
            XmlSerializer serializer = new XmlSerializer(typeof(XmlLanguageData));

            using (FileStream fs = File.OpenRead(m_SourceFilePath))
            {
                languageData = (XmlLanguageData)serializer.Deserialize(fs);
            }

            prepareFastAccessCaches(languageData);

            m_IsLoaded = true;
        }

        private void prepareFastAccessCaches(XmlLanguageData languageData)
        {
            var _translationGroups = languageData.Groups;
            m_HasSingleGroup = _translationGroups.Count == 1;

            if(m_HasSingleGroup)
            {
                m_SingleGroupCache = createCacheFromGroup(languageData.Groups[0]);
            }
            else
            {
                m_MultiGroupCache = new Dictionary<string, Dictionary<string, string>>(languageData.Groups.Count);

                for (int i = 0; i < languageData.Groups.Count; i++)
                    m_MultiGroupCache.Add(languageData.Groups[i].Name, createCacheFromGroup(languageData.Groups[i]));
            }
        }

        private Dictionary<string, string> createCacheFromGroup(XmlLanguageTranslationGroup group)
        {
            Dictionary<string, string> dictionaryCache = new Dictionary<string, string>(group.Items.Count);

            for(int i=0;i< group.Items.Count;i++)
            {
                var translationItem = group.Items[i];

                dictionaryCache.Add(translationItem.Name, translationItem.Value);
            }

            return dictionaryCache;
        }
    }
}
