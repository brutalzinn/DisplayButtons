using System;
using System.Collections.Generic;
using System.Text;

namespace DisplayButtons
{
    public class LibraryInfo
    {
        private string name;
        private string github_repo;

        public LibraryInfo(string name, string github_repo)
        {
            this.name = name;
            this.github_repo = github_repo;
        }

        public string Name { get => name; set => name = value; }
        public string Github_repo { get => github_repo; set => github_repo = value; }
    }
}
