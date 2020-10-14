using DisplayButtons.Backend.Objects.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public class PluginRunner
    {
        public class PluginMessageEventArgs
        {
            public DynamicDeckFolder folder { get; set; }
        }

        public event EventHandler<PluginMessageEventArgs> PluginMessage;

       

        public void SetFolder(DynamicDeckFolder s)
        {
            PluginMessage?.Invoke(this, new PluginMessageEventArgs { folder = s });
        }

    }
}
