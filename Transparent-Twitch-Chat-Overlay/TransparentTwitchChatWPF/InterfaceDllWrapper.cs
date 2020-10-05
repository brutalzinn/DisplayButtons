using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace TransparentTwitchChatWPF
{
    public class InterfaceDllWrapper : InterfaceDll.InterfaceDllClass
    {
    
        public void ActionMethod()
        {
            throw new NotImplementedException();
        }

        public void ButtonDown()
        {
           
            //instance.ExitApplication();
        }

        public void ButtonUp()
        {
        new MainWindow().Show();
        }

        public string GetDllName()
        {
            return "Chat box";
        }
    }
}
