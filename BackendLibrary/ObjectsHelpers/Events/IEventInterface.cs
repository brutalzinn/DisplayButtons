using BackendLibrary.ObjectsHelpers.Events;
using System;

public interface IEventInterface
{
    event EventHandler<DeviceStatusArgs> DeviceStatusChanged;

    void RaiseEvent(int value);
}