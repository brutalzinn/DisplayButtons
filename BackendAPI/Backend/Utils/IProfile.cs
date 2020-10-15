using BackendAPI.Objects;
using BackendAPI.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BackendAPI.Utils
{
    [Serializable]
    public  class Profile
        {
        private string _name;
        private MatrizObject _matriz;
        private IDeckFolder _mainfolder;
        
        private IDeckFolder _currentfolder; 
        public Profile() { }

        public Profile(string name, MatrizObject matriz, IDeckFolder mainfolder, IDeckFolder currentfolder = null)
        {
            _name = name;
            _matriz = matriz;
            _mainfolder = mainfolder;
            _currentfolder = currentfolder;
        }

        public string Name { get => _name; set => _name = value; }
        public MatrizObject Matriz { get => _matriz; set => _matriz = value; }
        public IDeckFolder Mainfolder { get => _mainfolder; set => _mainfolder = value; }
        [XmlIgnore]
        public IDeckFolder Currentfolder { get => _currentfolder; set => _currentfolder = value; }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            Profile p = obj as Profile;
            if ((System.Object)p == null)
                return false;

            return (Name == p.Name);
        }

        public bool Equals(Profile p)
        {
           if ((object)p == null)
             return false;

            return (Name == p.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
