using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    public interface ILanguagePackageSource
    {
        IList<ILanguagePackage> GetAllPackages();
        void LoadPackage(ILanguagePackage pack);
    }
}
