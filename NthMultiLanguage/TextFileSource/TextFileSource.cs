using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NthDeveloper.MultiLanguage
{
    public class TextFileSource : ILanguagePackageSource
    {
        private string m_FolderPath;
        private string m_FileExtension;

        public TextFileSource(string folderPath, string fileExtension = ".txt")
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
                packageList.Add(TextLanguagePackage.LoadOnlyWithNameAndCodeFromFile(filePath));
            }

            return packageList;
        }

        public void LoadPackage(ILanguagePackage pack)
        {
            ((TextLanguagePackage)pack).LoadFullData();
        }

        private string[] getAllFiles()
        {
            return Directory.GetFiles(m_FolderPath, "*" + m_FileExtension);
        }
    }
}
