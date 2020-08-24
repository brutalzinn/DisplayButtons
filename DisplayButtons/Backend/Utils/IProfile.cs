using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayButtons.Backend.Utils
{
    [Serializable]
    public  class Profile
        {
        private string name;
        private MatrizObject matriz;
        private IDeckFolder mainfolder;
        public Profile() { }
        public Profile(string name, MatrizObject matriz, IDeckFolder _mainfolder )
        {
            this.name = name;
            this.matriz = matriz;
            this.mainfolder = _mainfolder;
        }

        public string Name { get => name; set => name = value; }
        public MatrizObject Matriz { get => matriz; set => matriz = value; }
        public IDeckFolder Mainfolder { get => mainfolder; set => mainfolder = value; }
   
    }
}
