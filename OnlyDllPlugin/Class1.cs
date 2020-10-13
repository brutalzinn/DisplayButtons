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

        public void OnButtonDown()
        {
            Debug.WriteLine("Me apertou");
        }

        public void OnButtonUp()
        {
            Debug.WriteLine("apertou de fora");
        }
    }
}
