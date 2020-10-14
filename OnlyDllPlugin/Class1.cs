

using Backend.Objects;
using System;
using System.Diagnostics;

namespace OnlyDllPlugin
{
    public class Class1 : InterfaceDll.ButtonInterface
    {
        public string GetActionName()
        {
            return "Meu plugin";
        }

        public void MenuHelper()
        {
            new MyForm().Show();
        }

        public void OnButtonDown(DeckDevice device)
        {
            Debug.WriteLine($"Me apertou down {device.DeviceName}" );
        }

        public void OnButtonUp(DeckDevice device)
        {
            Debug.WriteLine($"apertou  up {device.DeviceName}" );
        }
    }
}
