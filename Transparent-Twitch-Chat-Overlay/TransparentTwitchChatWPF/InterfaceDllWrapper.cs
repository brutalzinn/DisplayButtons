using InterfaceDll;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using MoonSharp.Interpreter;
using TransparentTwitchChatWPF.Wrappers;
using NthDeveloper.MultiLanguage;
using System.IO;
using System.Reflection;

namespace TransparentTwitchChatWPF
{
   
    public class InterfaceDllWrapper : InterfaceDll.InterfaceDllClass
    {
       

        public string GetActionName() { return "Chat box v1"; }
        

        public void Configure(ServiceCollection service)
        {
            service.AddSingleton<IMyService>(new Service());
           
        }
        public void SetLang(string lang)
        {

            string directory = Path.GetFullPath($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\langs");
           
           if(!Directory.Exists(directory)){
                Directory.CreateDirectory(directory);
            }

         

            XmlFileSource _xmlFileSource = new XmlFileSource(directory);
            MultiLanguageProvider _languageProvider = new MultiLanguageProvider(_xmlFileSource);

          

            _languageProvider.SetCurrentLanguage(lang);

            Utilities.m_LanguageProvider = _languageProvider;
        }
        public void LoadScripts(Script script)
        {

            UserData.RegisterType<LibraryInterfaceWrapper>();
            DynValue chatboxclass = UserData.Create(new LibraryInterfaceWrapper());
            script.Globals.Set("chatbox", chatboxclass);

           
      


        }
    }
   public class Service : IMyService
        {
        
        public string Teste()
        {

            return "Escrevendo no console..";
        }
  
            

        }
}
