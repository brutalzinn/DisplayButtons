
using DisplayButtons.DeckEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DisplayButtons.Bibliotecas.DeckEvents
{
    public static class EventXml
    {
        private static AppSettings settings;

        public static AppSettings Settings
        {
            get
            {
                return settings;
            }
        }

        private const string SETTINGS_FILE = "events.xml";

        public static void LoadSettings()
        {
            try
            {
                if (File.Exists(SETTINGS_FILE))
                {
                    settings = XMLDisplayUtils.FromXMLEvents<AppSettings>(File.ReadAllText(SETTINGS_FILE));
                    return;
                }
            }
            catch (Exception)
            {
                //An error occured while loading the file.
                //Trying to delete the file.

                try
                {
                 File.Delete(SETTINGS_FILE);
                }
                catch (Exception)
                {
                    //Unable to delete file.
                    //Giving up on humanity.
                }
            }

            settings = new AppSettings();

        }

        public static void SaveSettings()
        {
            File.WriteAllText(SETTINGS_FILE, XMLDisplayUtils.ToXmlEvents(settings));
        }




    }

    [Serializable]
    public class AppSettings
    {
      
        /// <summary>
        /// Called to signal to subscribers that the theme was changed.
        /// </summary>



        public List<Event> Events { get; set; } = new List<Event>();



    }
}
