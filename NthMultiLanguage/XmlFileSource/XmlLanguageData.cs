using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NthDeveloper.MultiLanguage
{
    [XmlRoot("Language")]
    public class XmlLanguageData
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlArrayItem("Group")]
        public List<XmlLanguageTranslationGroup> Groups { get; set; }
    }
}
