using System;
using System.Collections.Generic;
using System.Text;

namespace BackendLibrary.ObjectsHelpers.Events
{
    public class DeviceStatusArgs : EventArgs
    {
        public DeviceStatusArgs(int value)
        {
            ChangeInteger = value;
        }

        public int ChangeInteger { get; private set; }
    }
}
