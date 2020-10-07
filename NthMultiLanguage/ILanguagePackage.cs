using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    public interface ILanguagePackage
    {
        string LanguageCode { get; }
        string LanguageName { get; }
        bool IsLoaded { get; }

        string GetString(string key);
        string GetString(string groupName, string key);
    }
}
