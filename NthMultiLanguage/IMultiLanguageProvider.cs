using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    public delegate void LanguagePackageChangedHandler(IMultiLanguageProvider sender, ILanguagePackage package);

    public interface IMultiLanguageProvider
    {
        event LanguagePackageChangedHandler CurrentPackageChanged;

        ILanguagePackage CurrentPackage { get; }
        IList<ILanguagePackage> AllPackages { get; }

        void SetCurrentLanguage(string languageCode);

        string GetString(string key);
        string GetString(string groupName, string key);
    }
}
