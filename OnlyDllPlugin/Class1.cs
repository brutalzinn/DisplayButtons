using Backend.Objects;
using System;
using System.Diagnostics;

namespace OnlyDllPlugin
{
    public class Class1 : Backend.Sdk.ButtonInterface
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
            Debug.WriteLine($"Apertou down {device.DeviceName}");
        }

        public void OnButtonUp(DeckDevice device)
        {
            Debug.WriteLine($"apertou up {device.DeviceName}");
        }
    }
}
