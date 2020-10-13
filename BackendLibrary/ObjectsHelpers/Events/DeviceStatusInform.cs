using System;
using System.Collections.Generic;
using System.Text;

namespace BackendLibrary.ObjectsHelpers.Events
{
    public  class DeviceStatusInform : IEventInterface
    {
        public event EventHandler<DeviceStatusArgs> DeviceStatusChanged;

        public void RaiseEvent(int value)
        {
            if (DeviceStatusChanged != null)
                DeviceStatusChanged(this, new DeviceStatusArgs(value));
        }
    }
}
