using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace TransparentTwitchChatWPF
{
    public interface InterfaceDllWrapper : InterfaceDll.InterfaceDllClass
    {


        public abstract void MostrarForm();
       

    }
    class DllWrapper : InterfaceDllWrapper
    {
        public string Name
        {
            get { return "Math"; }
        }

        public string Version
        {
            get { return "1.0.0.0"; }
        }

        public void MostrarForm();
        {

         new MainWindow().Show();

         }
    }
}

