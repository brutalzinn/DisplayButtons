using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NthDeveloper.MultiLanguage
{
    public class XmlLanguageTranslationGroup
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Item")]
        public List<XmlLanguageTranslationItem> Items { get; set; }
    }
}
