using DisplayButtons.Backend.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Backend
{
   public interface IGlobalHotkeys
    {

        void RegisterUniqueId(DynamicDeckFolder folder);
       void RemoveHotKey(DynamicDeckFolder folder);
        void refreshFolder(DynamicDeckFolder folder);
    }
}
