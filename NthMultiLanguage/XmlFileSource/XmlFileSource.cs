using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    public class XmlFileSource : ILanguagePackageSource
    {
        string m_FolderPath;
        string m_FileExtension;

        public XmlFileSource(string folderPath, string fileExtension=".xml")
        {
            m_FolderPath = folderPath;
            m_FileExtension = fileExtension;

            if (!m_FileExtension.StartsWith("."))
                m_FileExtension = "." + m_FileExtension;
        }

        public IList<ILanguagePackage> GetAllPackages()
        {
            string[] foundFiles = getAllFiles();

            List<ILanguagePackage> packageList = new List<ILanguagePackage>(foundFiles.Length);

            foreach (string filePath in foundFiles)
            {
               packageList.Add(XmlLanguagePackage.LoadOnlyWithNameAndCodeFromFile(filePath));
            }

            return packageList;
        }

        public void LoadPackage(ILanguagePackage pack)
        {
            ((XmlLanguagePackage)pack).LoadFullData();
        }     
        
        private string[] getAllFiles()
        {
            return Directory.GetFiles(m_FolderPath, "*" + m_FileExtension);
        }
    }
}
