using ButtonDeck.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ButtonDeck.Backend.Objects
{

 

        [Serializable]
    public abstract class AbstractDeckInformation
        {
            public static Type FindType(string fullName)
            {
                return
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => !a.IsDynamic)
                        .SelectMany(a => a.GetTypes())
                        .FirstOrDefault(t => t.FullName.Equals(fullName));
            }

            public abstract string GetActionName();
            public abstract string Color();

            public abstract int Size();
            public virtual bool IsPlugin()
            {

                return false;
            }
  
        }

    }
