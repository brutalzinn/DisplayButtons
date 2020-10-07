using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    public class MultiLanguageProvider : IMultiLanguageProvider
    {
        private List<ILanguagePackage> m_AllPackages;
        private ILanguagePackage m_CurrentPackage;
        private ILanguagePackageSource m_PackageSource;

        public event LanguagePackageChangedHandler CurrentPackageChanged;

        public IList<ILanguagePackage> AllPackages { get; private set; }

        public ILanguagePackage CurrentPackage
        {
            get { return m_CurrentPackage; }
            private set
            {
                if (value == m_CurrentPackage)
                    return;

                m_CurrentPackage = value;

                if (CurrentPackageChanged != null)
                    CurrentPackageChanged(this, m_CurrentPackage);
            }
        }

        public MultiLanguageProvider(ILanguagePackageSource packageSource)
        {
            m_PackageSource = packageSource;

            m_AllPackages = new List<ILanguagePackage>();
            this.AllPackages = m_AllPackages.AsReadOnly();

            m_AllPackages.AddRange(m_PackageSource.GetAllPackages());
        }

        public void SetCurrentLanguage(string languageCode)
        {
            ILanguagePackage package = m_AllPackages.FirstOrDefault(x => x.LanguageCode == languageCode);
            if (package == null)
                throw new Exception("Specified language package could not be found.");

            m_PackageSource.LoadPackage(package);

            this.CurrentPackage = package;
        }

        public string GetString(string key)
        {
            return m_CurrentPackage.GetString(key);
        }

        public string GetString(string groupName, string key)
        {
            return m_CurrentPackage.GetString(groupName, key);
        }
    }
}
