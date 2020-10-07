using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    class TextLanguagePackage : ILanguagePackage
    {
        private bool m_IsLoaded;
        private string m_SourceFilePath;
        private bool m_HasSingleGroup;

        private Dictionary<string, string> m_SingleGroupCache;
        private Dictionary<string, Dictionary<string, string>> m_MultiGroupCache;

        public bool IsLoaded { get { return m_IsLoaded; } }

        public string LanguageCode { get; private set; }

        public string LanguageName { get; private set; }

        private TextLanguagePackage(string sourceFilePath, string languageCode, string languageName)
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

            foreach (string groupName in m_MultiGroupCache.Keys)
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

        internal static TextLanguagePackage LoadOnlyWithNameAndCodeFromFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string lineText = null;
                TextFileLineType lineType = TextFileLineType.Unknown;

                //Read until file header
                while (!reader.EndOfStream)
                {
                    lineText = reader.ReadLine();
                    lineType = getLineType(lineText);

                    if (lineType == TextFileLineType.FileHeader)
                        break;
                }

                if (lineType != TextFileLineType.FileHeader)
                    return null;

                string languageName = null;
                string languageCode = null;

                while (!reader.EndOfStream)
                {
                    string nextKeyName;
                    string nextKeyValue;

                    if(readNextKey(reader, out nextKeyName, out nextKeyValue))
                    {
                        if (nextKeyName == "Name")
                            languageName = nextKeyValue;
                        else if (nextKeyName == "Code")
                            languageCode = nextKeyValue;

                        if (languageName != null && languageCode != null)
                            break;
                    }
                }

                return new TextLanguagePackage(filePath, languageCode, languageName);
            }
        }

        internal void LoadFullData()
        {
            if (m_IsLoaded)
                return;

            m_MultiGroupCache = new Dictionary<string, Dictionary<string, string>>();

            using (StreamReader reader = new StreamReader(m_SourceFilePath))
            {
                int groupCount = 0;
                Dictionary<string, string> currentGroupCache = null;

                while (!reader.EndOfStream)
                {
                    string lineText = reader.ReadLine();
                    TextFileLineType lineType = getLineType(lineText);

                    if(lineType == TextFileLineType.KeyValue)
                    {
                        if (currentGroupCache != null)
                        {
                            string[] arrlineParts = lineText.Split('=');

                            currentGroupCache.Add(arrlineParts[0], arrlineParts[1]);
                        }
                    }
                    else if(lineType == TextFileLineType.GroupHeader)
                    {
                        groupCount++;

                        string groupName;
                        int endIndex = lineText.IndexOf("]");
                        if (endIndex == 1)
                            groupName = "";

                        groupName = lineText.Substring(1, endIndex - 1);

                        currentGroupCache = new Dictionary<string, string>();
                        m_MultiGroupCache.Add(groupName, currentGroupCache);
                    }                    
                }

                if (groupCount == 1)
                {
                    m_SingleGroupCache = currentGroupCache;
                    m_HasSingleGroup = true;
                }
            }

            m_IsLoaded = true;
        }

        private static bool readNextKey(StreamReader reader, out string keyName, out string keyValue)
        {
            keyName = null;
            keyValue = null;

            while (!reader.EndOfStream)
            {
                string lineText = reader.ReadLine();
                TextFileLineType lineType = getLineType(lineText);

                if (lineType == TextFileLineType.KeyValue)
                {
                    string[] arrlineParts = lineText.Split('=');
                    keyName = arrlineParts[0];
                    keyValue = arrlineParts[1];
                    return true;                    
                }
            }

            return false;
        }

        private static TextFileLineType getLineType(string lineText)
        {
            if (String.IsNullOrWhiteSpace(lineText))
                return TextFileLineType.Empty;

            if (lineText.StartsWith("//"))
                return TextFileLineType.Comment;

            if (lineText.StartsWith("["))
                return TextFileLineType.GroupHeader;

            if (lineText.StartsWith("#"))
                return TextFileLineType.FileHeader;

            if (lineText.IndexOf('=') > 0)
                return TextFileLineType.KeyValue;

            return TextFileLineType.Unknown;
        }        
    }
}
